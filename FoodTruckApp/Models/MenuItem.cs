using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruckApp.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItem_ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Ingredient>? Ingredients { get; set; } = new List<Ingredient>();
        public virtual ICollection<Special>? Specials { get; set; } = new List<Special>();
        public virtual ICollection<Category>? Categories { get; set; } = new List<Category>();
    }
}
