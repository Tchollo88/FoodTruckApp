using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Repository.Data;
using Repository.Models.Menu;

namespace FoodTruckCustomer.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo _CustomerRepo;

        public CustomerController(ICustomerRepo customerRepo)
        {
            _CustomerRepo = customerRepo;
        }


        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Items(int? orderID, int cart)
        {
            ViewBag.cart = cart;
            if (orderID != null)
            {
                ViewBag.param = orderID;
            }
            else
            {
                var NewOrder = new Order();
                await _CustomerRepo.AddOrderAsync(NewOrder);
                ViewBag.param = NewOrder.Order_ID;
            }
            var items = await _CustomerRepo.GetAllItemsAsync();
            return View(items);
        }

        public async Task<IActionResult> Category(string category, int orderID, int cart)
        {
            ViewBag.param = orderID;
            ViewBag.secondParam = category;
            ViewBag.cart = cart;
            var categories = await _CustomerRepo.GetAllItemsAsync();
            if (category == null)
            {
                return RedirectToAction(nameof(Items), new { orderID = orderID});
            }
            return View(categories);
        }

        public async Task<IActionResult> NameSearch(string name, int orderID, int cart)
        {
            ViewBag.param = orderID;
            ViewBag.secondParam = name;
            ViewBag.cart = cart;
            var names = await _CustomerRepo.GetAllItemsAsync();
            if (name == null)
            {
                return RedirectToAction(nameof(Items), new { orderID = orderID });
            }
            return View(names);
        }

        public async Task<IActionResult> Details(int itemID, int orderID, int cart)
        {
            ViewBag.param = orderID;
            ViewBag.cart = cart;
            var item = await _CustomerRepo.GetItemByIdAsync(itemID);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
    }
}
