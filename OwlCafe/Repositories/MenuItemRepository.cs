using Microsoft.EntityFrameworkCore;
using OwlCafe.Data;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MenuItem>> GetAvailableMenuItemsAsync()
        {
            return await _dbSet
                .Where(m => m.Status && m.IsAvailable)
                .Include(m => m.Category)
                .OrderBy(m => m.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(m => m.CategoryId == categoryId && m.Status && m.IsAvailable)
                .OrderBy(m => m.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetFeaturedMenuItemsAsync()
        {
            return await _dbSet
                .Where(m => m.Status && m.IsAvailable && m.IsFeatured)
                .OrderBy(m => m.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .Where(m => m.Status)
                .Include(m => m.Category)
                .OrderByDescending(m => m.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalMenuItemsCountAsync()
        {
            return await _dbSet.Where(m => m.Status).CountAsync();
        }
    }
}
