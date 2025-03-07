using System.ComponentModel.DataAnnotations;
using FoodTruckApp.Models.Client;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.Menu
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }

        [Required]
        public virtual ICollection<Item>? Items { get; set; } = new List<Item>();

        [ForeignKey("Special")]
        public int Special_ID { get; set; }
        public virtual Special? special { get; set; }

        [Required]
        public int Amount { get; set; } = int.MinValue;
    }
}
