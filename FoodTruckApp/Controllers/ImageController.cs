using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models.Menu;

[Route("api/[controller]")]
[ApiController]
public class ItemImageController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ItemImageController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("{itemId}/upload-image")]
    public async Task<IActionResult> UploadImage(int itemId)
    {
        var file = Request.Form.Files["file"];
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(fileExtension))
        {
            return BadRequest("Invalid file type.");
        }

        var item = await _context.Items.FindAsync(itemId);
        if (item == null)
        {
            return NotFound("Item not found.");
        }

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // Delete old image if it exists
        if(!string.IsNullOrWhiteSpace(item.Image))
        {
            var oldFilePath = Path.Combine(uploadsFolder, item.Image);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
        }

        var uniqueFileName = Guid.NewGuid().ToString("N") + fileExtension;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        item.Image = uniqueFileName;
        await _context.SaveChangesAsync();

        return Ok(new { ImageUrl = $"/images/{uniqueFileName}" });
    }
}