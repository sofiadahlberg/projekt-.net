using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models;

public class Customer {
     public int CustomerId { get; set; }
      public string? Firstname {get; set;}
        [Required]
        public string? Lastname {get; set;}
        [Required]
        public string? PhoneNumber {get; set;}
        public DateTime Date { get; set; }
   [Required]
        public bool ApiKeyRequired {get; set; } = false;

        // Navigation 
        
    public ICollection<Treatment>? Treatments { get; set; }
}