using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Cart
{
    public class ShoppingCart
    {
        public readonly List<CartItem> _items;

        public ShoppingCart()
        {
            _items = new List<CartItem>();
        }

        public List<CartItem> Items => _items;

        public void AddItem(CartItem item)
        {
            var existingItem = _items.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _items.Add(item);
            }
        }

        public void RemoveItem(int itemId)
        {
            var item = _items.FirstOrDefault(i => i.ItemId == itemId);
            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public decimal GetTotal()
        {
            return _items.Sum(item => item.Price * item.Quantity);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
