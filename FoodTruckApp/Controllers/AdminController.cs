using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Repository.Models.Menu;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                await _ItemRepository.AddItemAsync(item);
                return RedirectToAction(nameof(Items));
            }
            return View(item);
        }
    }
}
