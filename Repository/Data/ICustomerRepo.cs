using Repository.Models.Menu;


namespace Repository.Data
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task AddItemAsync(Order amount);
        Task UpdateItemAsync(Order amount);
        Task DeleteItemAsync(int id);
    }
}
