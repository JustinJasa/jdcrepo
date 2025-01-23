public class BookingDto
{
    public int DinnerId { get; set; }
    public string AttendeeName { get; set; } = string.Empty; // Required attendee name
    public string AttendeeNumber { get; set; } = string.Empty; // Required attendee number
    public string? Request { get; set; } // Optional special requests or allergies
    public int BookingCapacity { get; set; } // Number of attendees (1 or 2)
}