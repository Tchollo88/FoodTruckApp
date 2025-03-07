using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruckApp.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict to Admins only
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Looks for Views/Admin/Index.cshtml
        }
    }
}
