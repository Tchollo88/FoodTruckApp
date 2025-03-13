using System;
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
    public class ReceiptsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiptsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Receipts.Include(r => r.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Receipt(int receiptId)
        {
            var receipt = await _context.Receipts
                .Include(r => r.Order)
                    .ThenInclude(o => o.LineItems)
                        .ThenInclude(li => li.Item)
                .FirstOrDefaultAsync(r => r.Receipt_ID == receiptId);

            return View(receipt);
        }

        public async Task<IActionResult> Checkout()
        {
            var order = await _context.Orders
                .Include(o => o.LineItems)
                    .ThenInclude(li => li.Item)
                .FirstOrDefaultAsync();

            if (order == null || !order.LineItems.Any())
            {
                return RedirectToAction("Cart", "Cart");
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutPost(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.LineItems)
                    .ThenInclude(li => li.Item)
                .FirstOrDefaultAsync(o => o.Order_ID == orderId);

            var receipt = new Receipt
            {
                Order_ID = order.Order_ID,
                Order = order,
                Date = DateTime.UtcNow
            };


            _context.Receipts.Add(receipt);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Checkout", new { receiptId = receipt.Receipt_ID });
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.Order)
                .FirstOrDefaultAsync(m => m.Receipt_ID == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipts.Remove(receipt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptExists(int id)
        {
            return _context.Receipts.Any(e => e.Receipt_ID == id);
        }
    }
}
