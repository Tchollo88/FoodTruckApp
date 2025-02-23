using System.ComponentModel.DataAnnotations;
using FoodTruckApp.Models.WorkForce;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.Menu
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual ICollection<MenuItem>? MenuItems { get; set; } = new List<MenuItem>();
        public virtual ICollection<Special>? Specials { get; set; } = new List<Special>();

        [Required]
        public int Amount { get; set; } = int.MinValue;
    }
}
