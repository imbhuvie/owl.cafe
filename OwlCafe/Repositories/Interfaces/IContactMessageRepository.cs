using OwlCafe.Models;

namespace OwlCafe.Repositories.Interfaces
{
    public interface IContactMessageRepository : IGenericRepository<ContactMessage>
    {
        Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync();
        Task<IEnumerable<ContactMessage>> GetMessagesPaginatedAsync(int pageNumber, int pageSize);
        Task<int> GetTotalUnreadCountAsync();
    }
}
