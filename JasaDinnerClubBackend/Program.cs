using JasaDinnerClubBackend.Data;
using Microsoft.EntityFrameworkCore;
using JasaDinnerClubBackend.Models;
using Mapster;
using JasaDinnerClubBackend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<IFormFile>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
    // Define the security scheme for JWT Bearer tokens
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Enter 'Bearer' followed by a space and your JWT token.\nExample: 'Bearer abc123xyz'"
    });

    // Apply the security requirement globally
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}  // No specific scopes required
        }
    });
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                             .AllowAnyHeader() // Allow any HTTP headers
                             .AllowAnyMethod(); // Allow any HTTP methods (GET, POST, PUT, DELETE, etc.)   
                      });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure


// Map Endpoints

/***** Booking Requests ******/
app.MapGet("/bookings", async (AppDbContext db) => await db.Bookings.ToListAsync());
app.MapGet("/bookings/{id}", async (int id, AppDbContext db) =>
{
    var booking = await db.Bookings
                          .Include(b => b.DinnerEvent)
                          .Include(b => b.Attendee)
                          .Where(b => b.BookingId == id)
                          .ProjectToType<BookingDto>() // Use Mapster's projection
                          .FirstOrDefaultAsync();

    return booking is not null ? Results.Ok(booking) : Results.NotFound();
});
app.MapPost("/bookings", async (BookingDto dto, AppDbContext db) =>
{
    // Validate dinner
    var dinner = await db.DinnerEvents
        .Include(d => d.Bookings)
        .FirstOrDefaultAsync(d => d.DinnerId == dto.DinnerId);
    if (dinner is null) return Results.NotFound($"Dinner with ID {dto.DinnerId} not found.");
    if (dinner.Bookings.Count >= dinner.Capacity)
        return Results.BadRequest("The dinner is fully booked.");

    // Validate or create attendee
    var attendee = await db.Attendee.FirstOrDefaultAsync(a => a.AttendeeNumber == dto.AttendeeNumber);
    if (attendee is null)
    {
        attendee = new Attendee
        {
            AttendeeName = dto.AttendeeName,
            AttendeeNumber = dto.AttendeeNumber,
        };
        db.Attendee.Add(attendee);
        await db.SaveChangesAsync();
    }

    // Create booking
    var booking = new Booking
    {
        DinnerId = dto.DinnerId,
        AttendeeId = attendee.AttendeeId,
        Request = dto.Request,
        BookingCapacity = dto.BookingCapacity
    };
    db.Bookings.Add(booking);
    await db.SaveChangesAsync();

    // Return response
    return Results.Created($"/bookings/{booking.BookingId}", new
    {
        BookingId = booking.BookingId,
        DinnerName = dinner.Name,
        AttendeeName = attendee.AttendeeName,
        Request = booking.Request
    });
});

app.MapPut("/bookings/{id}", async (int id, [FromBody] BookingDto dto, AppDbContext db) =>
{
    var booking = await db.Bookings.FindAsync(id);
    if (booking is null) return Results.NotFound(new { Message = $"Booking with ID {id} not found." });

    booking.Request = dto.Request;

    try
    {
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while updating the booking: {ex.Message}");
    }
});

app.MapDelete("/bookings/{id}", async (int id, AppDbContext db) =>
{
    var booking = await db.Bookings.FindAsync(id);

    if (booking is null)
    {
        return Results.NotFound(new { Message = $"Booking with ID {id} not found." });
    }

    try
    {
        db.Bookings.Remove(booking);
        await db.SaveChangesAsync();
        return Results.Ok(new { Message = $"Booking with ID {id} has been deleted." });
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while deleting the booking: {ex.Message}");
    }
}).RequireAuthorization();

/***** Dinner Requests ******/
app.MapGet("/dinners", async (AppDbContext db) => await db.DinnerEvents.ToListAsync());
app.MapGet("/dinners/{id}", async (int id, AppDbContext db) => await db.DinnerEvents.FindAsync(id) is DinnerEvent dinner ? Results.Ok(dinner) : Results.NotFound());
app.MapGet("/dinners/{id}/bookings", async (int id, AppDbContext db) => await db.Bookings.Where(b => b.DinnerId == id).ToListAsync());
app.MapGet("/dinners/{id}/attendees", async (int id, AppDbContext db) =>
{
    var attendees = await db.Bookings
        .Where(b => b.DinnerId == id)          // Filter by DinnerID
        .Select(b => b.Attendee)               // Select the related Attendees
        .ToListAsync();                        // Execute the query and retrieve as a list

    return attendees.Any() ? Results.Ok(attendees) : Results.NotFound();
});
app.MapGet("/dinners/{id}/capacity", async (int id, AppDbContext db) => await db.DinnerEvents.FindAsync(id) is DinnerEvent dinner ? Results.Ok(dinner.Capacity) : Results.NotFound());
app.MapPost("/dinners", async ([FromForm] DinnerEventDto dto, AppDbContext db) =>
{
    // Parse date and time as needed
    if (!DateTime.TryParse(dto.Date, out var parsedDate))
        return Results.BadRequest("Invalid date format. Use dd/MM/yyyy.");
    if (!TimeSpan.TryParse(dto.Time, out var parsedTime))
        return Results.BadRequest("Invalid time format. Use HH:mm:ss.");

    // Process the uploaded image using ImageHelper
    var imagePath = await ImageHelper.SaveImage(dto.ImageFile) ?? "images/default.jpg";

    var dinner = new DinnerEvent
    {
        Name = dto.Name,
        Date = parsedDate,
        Time = parsedTime,
        Capacity = dto.Capacity,
        Description = dto.Description,
        ImagePath = imagePath
    };

    db.DinnerEvents.Add(dinner);
    await db.SaveChangesAsync();

    return Results.Created($"/dinners/{dinner.DinnerId}", dinner);
})
.Accepts<DinnerEventDto>("multipart/form-data").DisableAntiforgery();
app.MapPut("/dinners/{id}", async (int id, DinnerEventDto dto, AppDbContext db) =>
{
    // Validate date format
    if (!DateTime.TryParse(dto.Date, out var parsedDate))
        return Results.BadRequest("Invalid date format. Use dd/MM/yyyy.");

    // Validate time format
    if (!TimeSpan.TryParse(dto.Time, out var parsedTime))
        return Results.BadRequest("Invalid time format. Use HH:mm:ss.");

    // Find the dinner event by ID
    var dinner = await db.DinnerEvents.FindAsync(id);
    if (dinner is null)
        return Results.NotFound();

    // Update dinner event properties
    dinner.Name = dto.Name;
    dinner.Date = parsedDate;
    dinner.Time = parsedTime;
    dinner.Capacity = dto.Capacity;
    dinner.Description = dto.Description;

    // Save changes to the database
    await db.SaveChangesAsync();

    // Return NoContent to indicate successful update
    return Results.NoContent();
}).RequireAuthorization();

app.MapDelete("/dinners/{id}", async (int id, AppDbContext db) =>
{
    if (await db.DinnerEvents.FindAsync(id) is DinnerEvent dinner)
    {
        db.DinnerEvents.Remove(dinner);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
}).RequireAuthorization();

/***** Attendees Requests ******/
app.MapGet("/attendees", async (AppDbContext db) => await db.Attendee.ToListAsync());
app.MapGet("/attendees/{id}", async (int id, AppDbContext db) => await db.Attendee.FindAsync(id) is Attendee attendee ? Results.Ok(attendee) : Results.NotFound());
app.MapGet("/attendees/{id}/bookings", async (int id, AppDbContext db) =>
{
    var attendeeWithBookings = await db.Bookings
        .Where(a => a.AttendeeId == id)
        .ProjectToType<BookingDto>() // Mapster projection
        .FirstOrDefaultAsync();

    return attendeeWithBookings is not null ? Results.Ok(attendeeWithBookings) : Results.NotFound();
});
app.MapGet("/attendees/{id}/dinners", async (int id, AppDbContext db) =>
{
    var dinners = await db.Bookings
        .Where(b => b.AttendeeId == id) // Filter by attendee ID
        .Select(b => b.DinnerEvent)     // Select the related DinnerEvent
        .ProjectToType<DinnerEventDto>() // Use Mapster to map to DinnerEventDto
        .ToListAsync();                 // Execute the query and get the results as a list

    return dinners.Any() ? Results.Ok(dinners) : Results.NotFound();
});
app.MapPost("/attendees", async (AttendeeDto dto, AppDbContext db) =>
{
    var attendee = new Attendee
    {
        AttendeeName = dto.AttendeeName,
        AttendeeNumber = dto.AttendeeNumber,
    };
    db.Attendee.Add(attendee);
    await db.SaveChangesAsync();
    return Results.Created($"/attendees/{attendee.AttendeeId}", attendee);
});
app.MapPut("/attendees/{id}", async (int id, AttendeeDto dto, AppDbContext db) =>
{
    var attendee = await db.Attendee.FindAsync(id);
    if (attendee is null) return Results.NotFound();
    attendee.AttendeeName = dto.AttendeeName;
    attendee.AttendeeNumber = dto.AttendeeNumber;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/attendees/{id}", async (int id, AppDbContext db) =>
{
    if (await db.Attendee.FindAsync(id) is Attendee attendee)
    {
        db.Attendee.Remove(attendee);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
}).RequireAuthorization();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // db.Database.Migrate(); // Automatically apply migrations
}
app.UseCors(MyAllowSpecificOrigins);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/images"
});


app.Run();
