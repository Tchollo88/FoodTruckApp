using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodTruckApp.Models.WorkForce;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.WorkForce
{
    public class Phone
    {
        [Key]
        public int Phone_ID { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty; // e.g., Mobile, Home, Work
    }
}
