using System.Reflection.Metadata;

namespace FoodTruckApp.Models.Menu
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        //public Blob Image { get; set; }
    }
}
