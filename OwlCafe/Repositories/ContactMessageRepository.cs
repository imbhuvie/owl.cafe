using Microsoft.EntityFrameworkCore;
using OwlCafe.Data;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Repositories
{
    public class ContactMessageRepository : GenericRepository<ContactMessage>, IContactMessageRepository
    {
        public ContactMessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync()
        {
            return await _dbSet
                .Where(m => !m.IsRead)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ContactMessage>> GetMessagesPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .OrderByDescending(m => m.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalUnreadCountAsync()
        {
            return await _dbSet.Where(m => !m.IsRead).CountAsync();
        }
    }
}
