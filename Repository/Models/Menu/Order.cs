using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models.Menu
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }

        [Required, ForeignKey("Item")]
        public int Item_ID { get; set; }
        public virtual Item? Item { get; set; }

        public bool Special { get; set; }

        public int Amount { get; set; } = int.MinValue;
    }
}
