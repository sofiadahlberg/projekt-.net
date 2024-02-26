using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models; 
public class Treatment{
    //Properties
    public int TreatmentId { get; set; }
    public int Duration { get; set; }
    public string? Category { get; set; }
       [Required]
    public bool ApiKeyRequired {get; set; } = false;

    //Foreign Keys
     public int CustomerId { get; set; }
    public int TimeIntervalId { get; set; }

     // Navigation 
    public Customer? Customer { get; set; }
    public TimeInterval? TimeInterval { get; set; }
}