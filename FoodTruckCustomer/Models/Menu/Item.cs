using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodTruckCustomer.Models.Menu
{
    public class Item
    {
        [Key]
        public int Item_ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;
    }
}
