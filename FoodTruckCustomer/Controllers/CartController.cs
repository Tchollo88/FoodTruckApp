﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public async Task<IActionResult> Cart(int orderID, int cart)
        {
            ViewBag.param = orderID;
            ViewBag.cart = cart;
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
            int cart = 0;
            foreach (var count in order.LineItems) 
            {
                cart += count.Quantity;
            };
            return RedirectToAction("Items", "Customer", new { orderID = orderID, cart = cart });
        }

        public IActionResult TransferView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TransferViewPost(int orderID, int cart)
        {
            return RedirectToAction("Items", "Customer", new { orderID = orderID, cart = cart });
        }

        [HttpPost]
        public async Task<IActionResult> AdjustCountAsync(int lineItemId, int qty, int orderID, int cart)
        {
            ViewBag.param = orderID;
            ViewBag.cart = cart;
            var lineItem = await _CustomerRepo.GetLineItemByIdAsync(lineItemId);

            lineItem.Quantity += qty;
            ViewBag.cart = cart += qty;

            if (lineItem.Quantity <= 0)
            {
                await _CustomerRepo.DeleteLineItemAsync(lineItemId);
            }
            else
            {
                await _CustomerRepo.UpdateLineItemAsync(lineItem);
            }
            return RedirectToAction("Cart", "Cart", new { orderID = orderID, cart = cart});
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int lineItemId, int orderID, int cart)
        {
            ViewBag.param = orderID;
            ViewBag.cart = cart;
            var lineItem = _CustomerRepo.GetLineItemByIdAsync(lineItemId);
            if (lineItem == null)
            {
                return NotFound();
            }
            return View(lineItem);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int lineItemId, int orderID, int cart)
        {
            ViewBag.param = orderID;
            cart = 0;
            await _CustomerRepo.DeleteLineItemAsync(lineItemId);
            var order = await _CustomerRepo.GetOrderByIdAsync(orderID);
            foreach (var count in order.LineItems)
            {
                cart += count.Quantity;
            };
            ViewBag.cart = cart;
            return RedirectToAction("Cart", "Cart", new { orderID = orderID, cart = cart});
        }

        public async Task<IActionResult> Checkout(int id)
        {
            var order = await _CustomerRepo.GetOrderByIdAsync(id);
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