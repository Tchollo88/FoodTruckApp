using System.ComponentModel.DataAnnotations;

namespace FoodTruckApp.Models
{
    public class Special
    {
        [Key]
        public int Special_ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
