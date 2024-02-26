using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models; 
public class TimeInterval{

    public int TimeIntervalId {get; set;}
      public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool Available { get; set; }
    [Required]
    public bool ApiKeyRequired {get; set; } = false;

// Navigation 
    public ICollection<Treatment>? Treatments { get; set; }
}