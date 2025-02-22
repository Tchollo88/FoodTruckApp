using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruckApp.Models
{
    public class Phone
    {
        [Key]
        public int Phone_ID { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty; // e.g., Mobile, Home, Work

        [Required, ForeignKey("User")]
        public int User_ID { get; set; }

        public virtual User? User { get; set; }
    }
}
