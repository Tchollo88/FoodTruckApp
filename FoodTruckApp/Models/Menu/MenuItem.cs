using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodTruckApp.Models.Client;
using FoodTruckApp.Models.Menu;


namespace FoodTruckApp.Models.Menu
{
    public class MenuItem
    {
        [Key]
        public int MenuItem_ID { get; set; }

        [Required, ForeignKey("User")]
        public int Item_ID { get; set; }

        public virtual Item? Item { get; set; }

        public virtual ICollection<Ingredient>? Ingredients { get; set; } = new List<Ingredient>();
    }
}
