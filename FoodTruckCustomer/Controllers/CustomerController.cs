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

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _CustomerRepo.GetItemByIdAsync(id);
            return View(item);
        }

        //GET: Customer/Create
        public ActionResult OrderCreate()
        {
            return View();
        }
        
        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order Amount)
        {
            if (ModelState.IsValid)
            {
                await _CustomerRepo.AddItemAsync(Amount);
                return RedirectToAction(nameof(Items));
            }
            return View(Amount);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _CustomerRepo.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order Amount)
        {
            if (ModelState.IsValid)
            {
                await _CustomerRepo.UpdateItemAsync(Amount);
                return RedirectToAction(nameof(Details), new { id = Amount.Order_ID });
            }
            return View(Amount);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Item = await _CustomerRepo.GetItemByIdAsync(id);
            if (Item == null)
            {
                return NotFound();
            }
            return View(Item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _CustomerRepo.DeleteItemAsync(id);
            return RedirectToAction(nameof(Items));
        }

    }
}
