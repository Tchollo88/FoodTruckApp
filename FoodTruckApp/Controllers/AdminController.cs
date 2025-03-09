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

        public async Task<IActionResult> Details(int id)
        {
            var Item = await _ItemRepository.GetItemByIdAsync(id);
            if (Item == null)
            {
                return NotFound();
            }
            return View(Item);
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

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _ItemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item Item)
        {
            if (ModelState.IsValid)
            {
                await _ItemRepository.UpdateItemAsync(Item);
                return RedirectToAction(nameof(Details), new { id = Item.Item_ID });
            }
            return View(Item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Item = await _ItemRepository.GetItemByIdAsync(id);
            if (Item == null)
            {
                return NotFound();
            }
            return View(Item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ItemRepository.DeleteItemAsync(id);
            return RedirectToAction(nameof(Items));
        }

        public async Task<IActionResult> Category(string category)
        {
            var categories = await _ItemRepository.GetCategoryAsync(category);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }
    }
}
