using OwlCafe.Models;

namespace OwlCafe.Repositories.Interfaces
{
    public interface IRestaurantTableRepository : IGenericRepository<RestaurantTable>
    {
        Task<IEnumerable<RestaurantTable>> GetAvailableTablesAsync();
        Task<IEnumerable<RestaurantTable>> GetTablesByCapacityAsync(int minCapacity);
        Task<RestaurantTable?> GetTableWithBookingsAsync(int id);
    }
}
