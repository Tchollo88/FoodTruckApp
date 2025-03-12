using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models.Menu;

namespace FoodTruckCustomer.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerRepo _CustomerRepo;

        public CartController(ApplicationDbContext context, ICustomerRepo customerRepo)
        {
            _context = context;
            _CustomerRepo = customerRepo;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var cartOrders = await _context.Orders.ToListAsync();
            return View(cartOrders);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var existingOrder = await _context.lineItems.FirstOrDefaultAsync(o => o.Item_ID == itemId);
            if (existingOrder != null)
            {
                existingOrder.Quantity++;
                _context.lineItems.Update(existingOrder);
            }
            else
            {
                var newOrder = new Order
                {
                    Item_ID = item.Item_ID,
                    Item = item,
                    Quantity = 1
                };
                _context.Orders.Add(newOrder);
            }
            await _context.SaveChangesAsync();

            // Redirect to CustomerController's Items action
            return RedirectToAction("Items", "CustomerController");
        }

        [HttpPost]
        public async Task<IActionResult> SingleAddAsync([Bind("Order_ID")] Order order) 
        {
            if (ModelState.IsValid)
            {
                var existingOrder = await _context.Orders.FindAsync(order.Order_ID);
                if (existingOrder != null)
                {
                    existingOrder.Quantity += 1; // Increment Quantity by 1
                    _context.Update(existingOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(Index);
        }

        [HttpPost]
        public async Task<IActionResult> SingleSubtractAsync(int orderId)
        {
            await _CustomerRepo.SubtractItemAsync(orderId);
            return RedirectToAction(nameof(Index)); // Refresh cart page
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Order_ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Order_ID == id);
        }
        public async Task<IActionResult> Checkout(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Order_ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Checkout")]
        public async Task<IActionResult> CheckoutConfirmed(int id)
        {
            //@ViewBag.OrderCount = orderCount;
            return View();

            //var order = await _context.Orders.FindAsync(id);
            //if (order != null)
            //{
            //    _context.Orders.Remove(order);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }
    }
}






/*
 * How to get  "public async Task<IActionResult> Checkout(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Order_ID == id);
            if (order == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
               await _CustomerRepo.AddItemAsync(orders);
                return RedirectToAction(nameof(Index));
            }
            return View(Index);
         }" to check out multiple orders in a list, move to a different view from a different controller, and delete the entire list
 */