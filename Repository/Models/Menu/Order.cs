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

        public virtual ICollection<lineItem> LineItems { get; set; } = new List<lineItem>();

        public decimal SubTotal
        {
            get 
            { 
                if (LineItems == null) return 0;
                return LineItems.Sum(li => li.Quantity * (li.Item?.Price ?? 0)); 
            }
        }
    }
}
