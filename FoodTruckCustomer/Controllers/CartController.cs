using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models.Menu;
using Repository.Models.Cart;

namespace FoodTruckCustomer.Controllers
{
    public class CartController : Controller
    {
        public readonly IItemRepository _itemRepository;
        public const string CartSessionKey = "ShoppingCart";

        public CartController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public ShoppingCart GetCart()
        {
            var cart = GetCart();
            if (cart == null)
            {
                cart = new ShoppingCart();
            }
            return cart;
        }

        public async Task<IActionResult> Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);
            if (item != null)
            {
                var cart = GetCart();
                cart.AddItem(new CartItem
                {
                    Item_Id = item.Item_ID,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = 1
                });
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var cart = GetCart();
            cart.RemoveItem(itemId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            // Implement checkout process here
            cart.Clear();
            return RedirectToAction("Index");
        }
    }
}

