using Microsoft.EntityFrameworkCore;
using OwlCafe.Data;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
        {
            return await _dbSet
                .Where(c => c.Status)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryWithMenuItemsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.MenuItems.Where(m => m.Status))
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
