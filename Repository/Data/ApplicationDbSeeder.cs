using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models.Menu;

public class ApplicationDbSeeder
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await SeedItemsAsync();
    }

    private async Task SeedItemsAsync()
    {
        var items = new List<Item>
        {
            new Item { Name = "Burger", Price = 5.99m, Category = "Main Course", Description = "Delicious burger", OnSale = false },
            new Item { Name = "Pizza", Price = 7.99m, Category = "Main Course", Description = "Cheesy pizza", OnSale = false },
            new Item { Name = "Fries", Price = 3.49m, Category = "Side", Description = "Crispy fries", OnSale = false }
        };

        foreach (var item in items)
        {
            if (!await _context.Items.AnyAsync(i => i.Name == item.Name))
            {
                _context.Items.Add(item);
            }
        }

        await _context.SaveChangesAsync();
    }
}