using Microsoft.EntityFrameworkCore;
using OwlCafe.Data;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Repositories
{
    public class RestaurantTableRepository : GenericRepository<RestaurantTable>, IRestaurantTableRepository
    {
        public RestaurantTableRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RestaurantTable>> GetAvailableTablesAsync()
        {
            return await _dbSet
                .Where(t => t.Status == TableStatus.Available)
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<RestaurantTable>> GetTablesByCapacityAsync(int minCapacity)
        {
            return await _dbSet
                .Where(t => t.Capacity >= minCapacity && t.Status == TableStatus.Available)
                .OrderBy(t => t.Capacity)
                .ToListAsync();
        }

        public async Task<RestaurantTable?> GetTableWithBookingsAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Bookings)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
