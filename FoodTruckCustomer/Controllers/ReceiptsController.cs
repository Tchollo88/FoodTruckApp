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
        private readonly ICustomerRepo _CustomerRepo;

        public ReceiptsController(ICustomerRepo context)
        {
            _CustomerRepo = context;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> Checkout(int orderID, int receiptID)
        {
            ViewBag.param = orderID;
            ViewBag.param2 = receiptID;
            var order = await _CustomerRepo.GetOrderByIdAsync(orderID);
            if (order == null)
            {
                return NotFound();
            }

            var receipt = new Receipt
            {
                Order_ID = orderID,
                Order = order,
                Date = DateTime.UtcNow
            };
            if (receiptID != 0)
            {
                receipt = await _CustomerRepo.GetReceiptByIdAsync(receiptID);
                await _CustomerRepo.AddReceiptAsync(receipt);
                if (receipt == null)
                {
                    return NotFound();
                }
            }

            return View(receipt);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutConfirmed(int orderID, int receiptID)
        {
            ViewBag.param = orderID;
            ViewBag.param2 = orderID;

            var order = await _CustomerRepo.GetOrderByIdAsync(orderID);
            if (order == null)
            {
                return NotFound();
            }

            var receipt = new Receipt
            {
                Order_ID = orderID,
                Order = order,
                Date = DateTime.UtcNow
            };

            return RedirectToAction("Checkout", "Receipts", new { orderID = orderID, receiptID = receiptID});
        }

    }

}
