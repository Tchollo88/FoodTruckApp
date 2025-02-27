using System.ComponentModel.DataAnnotations;

namespace FoodTruckCustomer.Models.Menu
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }

        [Required]
        public virtual ICollection<Item>? Items { get; set; } = new List<Item>();
        public virtual ICollection<Special>? Specials { get; set; } = new List<Special>();

        [Required]
        public int Amount { get; set; } = int.MinValue;
    }
}
