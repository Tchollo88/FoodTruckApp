using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var item = await _ItemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Item_ID,Name,Price,Description,Category,OnSale,Image")] Item item, IFormFile imageFile)
        {
            if (id != item.Item_ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingItem = await _ItemRepository.GetItemByIdAsync(id);
                    if (existingItem == null) return NotFound();

                    // Handle image upload
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("imageFile", "Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.");
                            return View(item);
                        }

                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "menu", "images");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Delete old image if it exists
                        if (!string.IsNullOrEmpty(existingItem.Image))
                        {
                            var oldFilePath = Path.Combine(uploadsFolder, existingItem.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // Save new image
                        var uniqueFileName = Guid.NewGuid().ToString("N") + fileExtension;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        existingItem.Image = uniqueFileName; // Update the Image property
                    }

                    // Update other fields
                    existingItem.Name = item.Name;
                    existingItem.Price = item.Price;
                    existingItem.Description = item.Description;
                    existingItem.Category = item.Category;
                    existingItem.OnSale = item.OnSale;

                    await _ItemRepository.UpdateItemAsync(existingItem);
                    return RedirectToAction(nameof(Details), new { id = item.Item_ID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                    return View(item);
                }
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _ItemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ItemRepository.DeleteItemAsync(id);
            return RedirectToAction(nameof(Items));
        }

        public async Task<IActionResult> Category(string category)
        {
            ViewBag.param = category;
            var categories = await _ItemRepository.GetAllItemsAsync();
            if (category == null)
            {
                return RedirectToAction(nameof(Items));
            }
            return View(categories);
        }

        public async Task<IActionResult> NameSearch(string name)
        {
            ViewBag.param = name;
            var names = await _ItemRepository.GetAllItemsAsync();
            if (name == null)
            {
                return RedirectToAction(nameof(Items));
            }
            return View(names);
        }

        public async Task<IActionResult> SalesReport()
        {
            var receipts = await _ItemRepository.GetAllReceiptsAsync();
            return View(receipts);
        }
    }
}