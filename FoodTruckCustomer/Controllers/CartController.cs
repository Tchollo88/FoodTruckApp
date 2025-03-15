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
        private readonly ICustomerRepo _CustomerRepo;

        public CartController(ICustomerRepo customerRepo)
        {
            _CustomerRepo = customerRepo;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Cart(int orderID)
        {
            ViewBag.param = orderID;
            var order = await _CustomerRepo.GetOrderByIdAsync(orderID);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int itemId, int qty, int orderID)
        {
            var item = await _CustomerRepo.GetItemByIdAsync(itemId);
            if (qty <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }
            var order = await _CustomerRepo.GetOrderByIdAsync(orderID);
            if (order.LineItems.Count == 0)
            {
                order.LineItems.Add
                (
                    new lineItem
                    {
                        Item_ID = item.Item_ID,
                        Item = item,
                        Order_ID = orderID,
                        Order = order,
                        Quantity = qty
                    }
                );
                await _CustomerRepo.UpdateOrderAsync(order);
            }
            else
            {
                var existingOrder = order.LineItems.FirstOrDefault(o => o.Item_ID == item.Item_ID);
                if (existingOrder != null)
                {
                    existingOrder.Quantity += qty;
                    await _CustomerRepo.UpdateLineItemAsync(existingOrder);
                }
                else
                {
                    order.LineItems.Add
                    (
                        new lineItem 
                        {
                            Item_ID = item.Item_ID,
                            Item = item, 
                            Quantity = qty 
                        }
                    );
                    await _CustomerRepo.UpdateOrderAsync(order);
                }
            }
            return RedirectToAction("Items", "Customer", new { orderID = orderID });
        }

        public IActionResult TransferView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TransferViewPost(int orderID)
        {
            return RedirectToAction("Items", "Customer", new { orderID = orderID });
        }

        [HttpGet]
        public async Task<IActionResult> AdjustCount(int lineItemId)
        {
             var lineItem = _CustomerRepo.GetLineItemByIdAsync(lineItemId);

            return View(lineItem);
        }

        [HttpPost]
        public async Task<IActionResult> AdjustCountAsync(int lineItemId, int qty, int orderID)
        {
            ViewBag.param = orderID;
            var lineItem = await _CustomerRepo.GetLineItemByIdAsync(lineItemId);

            lineItem.Quantity += qty;

            if (lineItem.Quantity <= 0)
            {
                await _CustomerRepo.DeleteLineItemAsync(lineItemId);
            }
            else
            {
                await _CustomerRepo.UpdateLineItemAsync(lineItem);
            }
            return RedirectToAction("Cart", "Cart", new { orderID = orderID});
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int lineItemId)
        {
            var lineItem = _CustomerRepo.GetLineItemByIdAsync(lineItemId);
            if (lineItem == null)
            {
                return NotFound();
            }
            return View(lineItem);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int lineItemId, int orderID)
        {
            await _CustomerRepo.DeleteLineItemAsync(lineItemId);
            return RedirectToAction("Cart", "Cart", new { orderID = orderID});
        }
    }
}
