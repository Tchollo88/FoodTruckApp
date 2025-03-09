using Repository.Models.Menu;

namespace Repository.Data
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task<IEnumerable<Item>> GetCategoryAsync(string category);
    }
}
