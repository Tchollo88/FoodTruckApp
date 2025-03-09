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
        public virtual Order? Item { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal(Item item, decimal Price)
        {
            if(Quantity > 1)
            {
                return item.Price * Quantity;
            }
            else
            {
                return item.Price;
            }
        }


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
