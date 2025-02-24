using System.ComponentModel.DataAnnotations;
using FoodTruckApp.Models.Client;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.Menu
{
    public class Special
    {
        [Key]
        public int Special_ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Discount { get; set; } = decimal.MinValue;
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }

    }
}
