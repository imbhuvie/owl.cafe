using OwlCafe.Models;

namespace OwlCafe.Repositories.Interfaces
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        Task<IEnumerable<MenuItem>> GetAvailableMenuItemsAsync();
        Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId);
        Task<IEnumerable<MenuItem>> GetFeaturedMenuItemsAsync();
        Task<IEnumerable<MenuItem>> GetMenuItemsWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> GetTotalMenuItemsCountAsync();
    }
}
