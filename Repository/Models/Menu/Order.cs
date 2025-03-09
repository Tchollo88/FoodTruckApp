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

        [Required]
        public virtual ICollection<Item>? _items { get; set; } = new List<Item>();


        public bool Special(bool _items, decimal _price, decimal Price)
        {
            
            if (!_items && _price == Price)
            {
                return true;
            }
            else if (_items)
            {
                _price = Price * 0.90m; 
                return false; 
            }
            return false;
        }

    }
}
