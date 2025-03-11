using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models.Menu
{
    public class Item
    {
        [Key]
        public int Item_ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } = decimal.Zero;

        public string Description { get; set; } = string.Empty;

        public bool Special { get; set; } = false;
        public int Discount { get; set; } = 10;

        [Required]
        public string Category { get; set; } = string.Empty;
    }
}
