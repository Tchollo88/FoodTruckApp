using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models.Menu;

namespace Repository.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }
        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Receipt>> GetAllReceiptsAsync()
        {
            return await _context.Receipts.ToListAsync();
        }

        public async Task<IEnumerable<Receipt>> GetAllReceiptsAsync(DateTime start, DateTime end)
        {
            return await _context.Receipts.
                Where(r => r.Date >= start && r.Date <= end).
                Include(o => o.Order).
                ThenInclude(li => li.LineItems).
                ThenInclude(i => i.Item).
                ToListAsync();
        }

        public async Task<Receipt> GetReceiptByIdAsync(int id)
        {
            return await _context.Receipts.
                Where(r => r.Receipt_ID == id).
                Include(o => o.Order).
                ThenInclude(li => li.LineItems).
                ThenInclude(i => i.Item).
                FirstOrDefaultAsync();
        }
    }
}
