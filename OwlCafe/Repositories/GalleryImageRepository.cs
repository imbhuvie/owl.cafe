using Microsoft.EntityFrameworkCore;
using OwlCafe.Data;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Repositories
{
    public class GalleryImageRepository : GenericRepository<GalleryImage>, IGalleryImageRepository
    {
        public GalleryImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GalleryImage>> GetActiveGalleryImagesAsync()
        {
            return await _dbSet
                .Where(g => g.Status)
                .OrderBy(g => g.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<GalleryImage>> GetGalleryImagesByCategoryAsync(string category)
        {
            return await _dbSet
                .Where(g => g.Status && g.Category == category)
                .OrderBy(g => g.DisplayOrder)
                .ToListAsync();
        }
    }
}
