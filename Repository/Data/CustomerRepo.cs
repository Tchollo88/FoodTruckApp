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

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
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

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.
                Where(o => o.Order_ID == id).
                Include(li => li.LineItems).
                ThenInclude(i => i.Item).
                FirstOrDefaultAsync();
        }

        public async Task<lineItem> GetLineItemByIdAsync(int id)
        {
            return await _context.lineItems.
                Where(li => li.lineItem_ID == id).
                Include(i => i.Item).
                FirstOrDefaultAsync();
        }

        public async Task DeleteLineItemAsync(int id)
        {
            var lineItem = await _context.lineItems.FindAsync(id);
            if (lineItem != null)
            {
                _context.lineItems.Remove(lineItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateLineItemAsync(lineItem lineItem)
        {
            _context.Entry(lineItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task AddLineItemAsync(lineItem lineItem)
        {
            _context.lineItems.Add(lineItem);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
