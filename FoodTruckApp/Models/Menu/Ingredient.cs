using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoodTruckApp.Models.WorkForce;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.Menu
{
    public class Ingredient
    {
        [Key]
        public int Ingredient_ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int NumberInStock { get; set; } = int.MinValue;
    }
}
