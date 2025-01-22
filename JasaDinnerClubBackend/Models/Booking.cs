using System.ComponentModel.DataAnnotations;

namespace JasaDinnerClubBackend.Models;
public class Booking
{

    [Required]
    public int BookingId {get; set;}

    [Required]
    public int DinnerId {get; set;}
    
    public DinnerEvent? DinnerEvent { get; set; } // Navigation property

    [Required]
    public int AttendeeId {get; set;}

    // Optional: Special requests or allergies
    public Attendee? Attendee { get; set; }// Navigation property

    public string? Request {get; set;}

    [Required]
    public int BookingCapacity {get; set;}

}