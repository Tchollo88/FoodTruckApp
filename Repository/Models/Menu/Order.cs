using System.ComponentModel.DataAnnotations;

namespace Repository.Models.Menu
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }

        [Required]
        public virtual ICollection<MenuItem>? MenuItems { get; set; } = new List<MenuItem>();
        public virtual ICollection<Special>? Specials { get; set; } = new List<Special>();

        [Required]
        public int Amount { get; set; } = int.MinValue;
    }
}
