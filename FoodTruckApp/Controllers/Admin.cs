using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Repository.Models.Client;
using Repository.Models.Menu;

namespace FoodTruckApp.Controllers
{
    public class Admin : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(Db);

        public ApplicationDbContext Db { get => db; set => db = value; }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("admin");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddItem()
        {
            if (HttpContext.Request.Cookies.ContainsKey("admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public IActionResult AddNewItem(string itemName, int price, string path)
        {
            if (HttpContext.Request.Cookies.ContainsKey("admin"))
            {

                Item newItem = new Item {Name = itemName, Price = price, ImagePath = path };

                ItemRepository repo = new ItemRepository(db);
                repo.AddItemAsync(newItem);

                return View("AddItem", "Item Added Successfully!!!");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult AllUsers()
        {
            return View();
        }

        public List<Customer> getAllUsers()
        {
            Console.WriteLine("Retrieving Customer List...");
            return db.Customers.ToList();
        }
    }
}
