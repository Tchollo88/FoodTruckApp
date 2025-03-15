using Repository.Models.Menu;


namespace Repository.Data
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task AddOrderAsync(Order item);
        Task<bool> SubtractItemAsync(int orderId);
        Task UpdateItemAsync(Order amount);
        Task DeleteItemAsync(int id);
        Task CheckoutAsync(List<Order> order);
        Task<Order> GetOrderByIdAsync(int id);
        Task<lineItem> GetLineItemByIdAsync(int id);
        Task DeleteLineItemAsync(int id);
        Task UpdateLineItemAsync(lineItem item);
        Task AddLineItemAsync(lineItem lineItem);
        Task UpdateOrderAsync(Order order);
    }
}
