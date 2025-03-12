using Repository.Models.Menu;


namespace Repository.Data
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task AddItemAsync(Order item);
        Task<bool> SubtractItemAsync(int orderId);
        Task DeleteItemAsync(int id);
        Task CheckoutAsync(List<Order> order);
    }
}
