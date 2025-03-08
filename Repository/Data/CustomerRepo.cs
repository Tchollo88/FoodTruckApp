using Microsoft.EntityFrameworkCore;
using Repository.Models.Menu;
using Microsoft.Data.SqlClient;

namespace Repository.Data
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepo(ApplicationDbContext context)
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

        public async Task AddItemAsync(Order amount)
        {
            _context.Orders.Add(amount);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Order amount)
        {
            _context.Entry(amount).State = EntityState.Modified;
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


    }
}
