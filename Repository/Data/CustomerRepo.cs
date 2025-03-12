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

        public async Task<bool> SubtractItemAsync(int orderId)
        {
            var existingOrder = await _context.lineItems.FindAsync(orderId);

            if (existingOrder == null)
            {
                return false;
            }

            if (existingOrder.Quantity > 1)
            {
                existingOrder.Quantity--;
                _context.lineItems.Update(existingOrder);
            }
            else
            {
                // If quantity is 0 or 1, remove the order
                _context.lineItems.Remove(existingOrder);
            }
            await _context.SaveChangesAsync();

            return true;
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


        public Task CheckoutAsync(List<Order> order)
        {
            throw new NotImplementedException();
        }
    }
}
