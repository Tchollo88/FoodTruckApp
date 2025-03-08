using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models.Menu
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }

        [Required]
        public virtual ICollection<MenuItem>? MenuItems { get; set; } = new List<MenuItem>();

        [Required, ForeignKey("Special")]
        public int Special_ID { get; set; }
        public virtual Special? Special { get; set; }

        public int Amount { get; set; } = int.MinValue;
    }
}
