using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;

namespace FoodTruckApp.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict to Admins only
    public class AdminController : Controller
    {
        private readonly IItemRepository _ItemRepository;
        public AdminController(IItemRepository itemRepository)
        {
            _ItemRepository = itemRepository;
        }
        public IActionResult Index()
        {
            return View(); // Looks for Views/Admin/Index.cshtml
        }
        public async Task<IActionResult> Items()
        {
            var items = await _ItemRepository.GetAllItemsAsync();
            return View(items);
        }
    }
}
