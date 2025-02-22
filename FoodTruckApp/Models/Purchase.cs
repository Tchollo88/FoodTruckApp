using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruckApp.Models
{
    public class Purchase
    {
        [Key]
        public int Purchase_ID { get; set; }

        [Required, ForeignKey("MenuItem")]
        public int MenuItem_ID { get; set; }
        public virtual MenuItem? MenuItem { get; set; }

        [Required, ForeignKey("Receipt")]
        public int Receipt_ID { get; set; }
        public virtual Receipt? Receipt { get; set; }
    }
}
