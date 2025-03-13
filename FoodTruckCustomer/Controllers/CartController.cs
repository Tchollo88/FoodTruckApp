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
            return View();
        }
        public async Task<IActionResult> Cart()
        {
            var orders = await _context.Orders
                .Include(o => o.LineItems)
                .ThenInclude(li => li.Item)
                .ToListAsync();

            ViewBag.Items = await _context.Items.ToListAsync();

            return View(orders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int itemId, int qty)
        {
            if (qty <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            var item = await _context.Items.FindAsync(itemId);
            var newOrder = new Order
            {
                LineItems = new List<lineItem>()
            };

            var newLineItem = new lineItem
            {
                Item_ID = itemId,
                Quantity = qty,
                Item = item,
                Order = newOrder
            };

            newOrder.LineItems.Add(newLineItem);
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction("Items", "Customer");
        }

        public IActionResult TransferView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TransferViewPost()
        {
            return RedirectToAction("Items", "Customer");
        }

        [HttpGet]
        public async Task<IActionResult> AdjustCount(int lineItemId)
        {
             var lineItem = await _context.lineItems
                .Include(li => li.Item)
                .FirstOrDefaultAsync(li => li.lineItem_ID == lineItemId);

            return View(lineItem);
        }

        [HttpPost]
        public async Task<IActionResult> AdjustCountAsync(int lineItemId, int qty)
        {
            var lineItem = await _context.lineItems
                .Include(li => li.Order)
                .Include(li => li.Item)
                .FirstOrDefaultAsync(li => li.lineItem_ID == lineItemId);

            lineItem.Quantity += qty;

            if (lineItem.Quantity <= 0)
            {
                var order = lineItem.Order;
                order.LineItems.Remove(lineItem);
                _context.lineItems.Remove(lineItem);

                if (!order.LineItems.Any())
                {
                    _context.Orders.Remove(order);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart", "Cart");
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? lineItemId)
        {
            var lineItem = await _context.lineItems
                            .Include(li => li.Item)
                            .FirstOrDefaultAsync(li => li.lineItem_ID == lineItemId);

            return View(lineItem);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int lineItemId)
        {
            var lineItem = await _context.lineItems
                .Include(li => li.Order)
                .Include(li => li.Item)
                .FirstOrDefaultAsync(li => li.lineItem_ID == lineItemId);

            var order = lineItem.Order;
            lineItem.Quantity = 0;
            if (lineItem.Quantity <= 0)
            {
                order = lineItem.Order;
                order.LineItems.Remove(lineItem);
                _context.lineItems.Remove(lineItem);

                if (!order.LineItems.Any())
                {
                    _context.Orders.Remove(order);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart", "Cart");
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






//How to get  "public async Task<IActionResult> Checkout(int? id) 
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var order = await _context.Orders
//                .FirstOrDefaultAsync(m => m.Order_ID == id);
//            if (order == null)
//            {
//                return NotFound();
//            }
//            if (ModelState.IsValid)
//            {
//               await _CustomerRepo.AddItemAsync(orders);
//                return RedirectToAction(nameof(Index));
//            }
//            return View(Index);
//         }" to check out multiple orders in a list, move to a different view from a different controller, and delete the entire list
 


//[HttpPost]
//public async Task<IActionResult> Create(int itemId)
//{
//    var item = await _context.Items.FindAsync(itemId);
//    if (item == null)
//    {
//        return NotFound();
//    }

//    var existingOrder = await _context.lineItems.FirstOrDefaultAsync(o => o.Item_ID == itemId);
//    if (existingOrder != null)
//    {
//        existingOrder.Quantity++;
//        _context.lineItems.Update(existingOrder);
//    }
//    else
//    {
//        var newOrder = new Order
//        {
//            Item_ID = item.Item_ID,
//            Item = item,
//            Quantity = 1
//        };
//        _context.Orders.Add(newOrder);
//    }
//    await _context.SaveChangesAsync();

//    // Redirect to CustomerController's Items action
//    return RedirectToAction("Items", "CustomerController");
//