using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Items()
        {
            var items = await _CustomerRepo.GetAllItemsAsync();
            return View(items);
        }

        public async Task<IActionResult> Category(string category)
        {
            ViewBag.param = category;
            var categories = await _CustomerRepo.GetAllItemsAsync();
            if (category == null)
            {
                return RedirectToAction(nameof(Items));
            }
            return View(categories);
        }

        public async Task<IActionResult> NameSearch(string name)
        {
            ViewBag.param = name;
            var names = await _CustomerRepo.GetAllItemsAsync();
            if (name == null)
            {
                return RedirectToAction(nameof(Items));
            }
            return View(names);
        }
    }
}
