using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Repository.Models.Menu
{
    public class Order
    { 
        [Key]
        public int Order_ID { get; set; }

        [Required, ForeignKey("Item")]
        public int Item_ID { get; set; }
        public virtual Item? Item { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal SubTotal 
        { get => Quantity * Price;}
    }
}
