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

            receipt.Receipt_ID = receiptID;

            return View(receipt);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutConfirmed(int orderID)
        {
            ViewBag.param = orderID;
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

            await _CustomerRepo.AddReceiptAsync(receipt);

            return RedirectToAction("Checkout", "Receipts", new { orderID = orderID, receiptID = receipt.Receipt_ID });

        }
    }

}

