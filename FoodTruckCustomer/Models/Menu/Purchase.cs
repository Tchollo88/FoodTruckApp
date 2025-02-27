using System.ComponentModel.DataAnnotations;

namespace FoodTruckCustomer.Models.Menu
{
    public class Purchase
    {
        [Key]
        public int Purchase_ID { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
