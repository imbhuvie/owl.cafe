using OwlCafe.Models;

namespace OwlCafe.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
        Task<Category?> GetCategoryWithMenuItemsAsync(int id);
    }
}
