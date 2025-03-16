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
    //Items
        var items = new List<Item>
        {
            new Item { Name = "Darkwood Brew", Price = 4.99m, Category = "Beverage", Description = "Hot mushroom coffee in a wood mug", OnSale = false },
            new Item { Name = "Fae Nectar", Price = 3.99m, Category = "Beverage", Description = "Honey sweetened ice mushroom tea, with edible flowers", OnSale = true },
            new Item { Name = "Forest Guardian Bowl", Price = 5.99m, Category = "Breakfast", Description = "Oatmeal topped with mushrooms", OnSale = true },
            new Item { Name = "Mossy Bites", Price = 5.99m, Category = "Appetizer", Description = "Stuffed mushrooms topped stuffed tomatos", OnSale = false },
            new Item { Name = "Mycellium Melt", Price = 8.99m, Category = "Main Course", Description = "Sourdough swiss melt with sauted onions and mushrooms", OnSale = false },
            new Item { Name = "Pixie Puffs", Price = 3.99m, Category = "Dessert", Description = "Fried cream cheese custerd breaded in dried mushroom flakes served with candied blueberry", OnSale = false },
            new Item { Name = "Spore Born Skewers", Price = 4.99m, Category = "Appetizer", Description = "Rosemarry kebabed mushrooms grilled with a teriyaki glaze", OnSale = false },
            new Item { Name = "Burger", Price = 5.99m, Category = "Main Course", Description = "Delicious burger", OnSale = false },
            new Item { Name = "Burger", Price = 5.99m, Category = "Main Course", Description = "Delicious burger", OnSale = false},
            new Item { Name = "Pizza", Price = 7.99m, Category = "Main Course", Description = "Cheesy pizza", OnSale = false },
            new Item { Name = "Fries", Price = 3.49m, Category = "Side", Description = "Crispy fries", OnSale = true }
        };

        foreach (var item in items)
        {
            if (!await _context.Items.AnyAsync(i => i.Name == item.Name))
            {
                _context.Items.Add(item);
            }
        }
    //Receipts
        var receipts = new List<Receipt>
        {
            new Receipt
            {
                Order_ID = 1,
                TotalPrice = 16.17m,
                Date = DateTime.Now.AddDays(-64)
            },
            new Receipt
            {
                Order_ID = 2,
                TotalPrice = 19.36m,
                Date = DateTime.Now.AddDays(-27)
            },
            new Receipt
            {
                Order_ID = 3,
                TotalPrice = 8.48m,
                Date = DateTime.Now.AddDays(-8)
            }
        };

        foreach (var receipt in receipts)
        {
            if (!await _context.Receipts.AnyAsync(r => r.Receipt_ID <= 3))
            {
                _context.Receipts.Add(receipt);
            }
        }
    //Orders
        var orders = new List<Order>
        {
            new Order
            {
            },
            new Order
            {
            },
            new Order
            {
            }
        };

        foreach (var order in orders)
        {
            if (!await _context.Orders.AnyAsync(o => o.Order_ID <= 3))
            {
                _context.Orders.Add(order);
            }
        }
    //LineItems
        var lineItems = new List<lineItem>
        {
            new lineItem
            {
                Item_ID = items[4].Item_ID,
                Item = items[4],
                Order_ID = 1,
                Quantity = 1
            },
            new lineItem
            {
                Item_ID = items[5].Item_ID,
                Item = items[5],
                Order_ID = 1,
                Quantity = 2
            },
            new lineItem
            {
                Item_ID = items[6].Item_ID,
                Item = items[6],
                Order_ID = 3,
                Quantity = 1
            },
            new lineItem
            {
                Item_ID = items[9].Item_ID,
                Item = items[9],
                Order_ID = 3,
                Quantity = 1
            },
            new lineItem
            {
                Item_ID = 1,
                Item = items[0],
                Order_ID = 2,
                Quantity = 1
            },
            new lineItem
            {
                Item_ID = 2,
                Item = items[1],
                Order_ID = 2,
                Quantity = 1
            },
            new lineItem
            {
                Item_ID = 3,
                Item = items[2],
                Order_ID = 2,
                Quantity = 2
            }
        };

        foreach (var lineItem in lineItems)
        {
            if (!await _context.lineItems.AnyAsync(li => li.lineItem_ID <= 7))
            {
                _context.lineItems.Add(lineItem);
            }
        }
        await _context.SaveChangesAsync();
    }
}