using Microsoft.EntityFrameworkCore;
using OwlCafe.Data;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetTodayBookingsAsync()
        {
            var today = NormalizeBookingDate(DateTime.Today);
            return await _dbSet
                .Where(b => b.BookingDate.Date == today && b.Status == BookingStatus.Approved)
                .Include(b => b.Table)
                .OrderBy(b => b.BookingTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDateAsync(DateTime date)
        {
            date = NormalizeBookingDate(date);
            return await _dbSet
                .Where(b => b.BookingDate.Date == date && b.Status == BookingStatus.Approved)
                .Include(b => b.Table)
                .OrderBy(b => b.BookingTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetAvailableTablesAsync(DateTime date, TimeSpan time, int guests)
        {
            date = NormalizeBookingDate(date);
            var bookedTables = await _dbSet
                .Where(b => b.BookingDate.Date == date && 
                           b.BookingTime == time && 
                           (b.Status == BookingStatus.Approved || b.Status == BookingStatus.Pending))
                .Select(b => b.TableId)
                .ToListAsync();

            return await _dbSet
                .Include(b => b.Table)
                .Where(b => b.Table != null && 
                           b.Table.Capacity >= guests && 
                           !bookedTables.Contains(b.TableId))
                .ToListAsync();
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime date, TimeSpan time)
        {
            date = NormalizeBookingDate(date);
            return !await _dbSet
                .AnyAsync(b => b.TableId == tableId && 
                              b.BookingDate.Date == date && 
                              b.BookingTime == time && 
                              (b.Status == BookingStatus.Approved || b.Status == BookingStatus.Pending));
        }

        public async Task<IEnumerable<Booking>> GetBookingsByStatusAsync(BookingStatus status)
        {
            return await _dbSet
                .Where(b => b.Status == status)
                .Include(b => b.Table)
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingWithTableAsync(int id)
        {
            return await _dbSet
                .Include(b => b.Table)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        private static DateTime NormalizeBookingDate(DateTime date)
        {
            return DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        }
    }
}
