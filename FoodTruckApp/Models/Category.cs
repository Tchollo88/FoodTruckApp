using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTruckApp.Models
{
    public class Category
    {
        [Key]
        public int Category_ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<MenuItem>? MenuItems { get; set; } = new List<MenuItem>();
    }
}
