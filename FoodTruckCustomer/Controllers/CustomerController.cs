using Microsoft.AspNetCore.Mvc;

namespace FoodTruckCustomer.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult ItemIndex()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult ItemDetails(int id)
        {
            return View();
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
    }
}
