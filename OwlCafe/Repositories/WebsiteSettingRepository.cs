using Microsoft.EntityFrameworkCore;
using OwlCafe.Data;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Repositories
{
    public class WebsiteSettingRepository : GenericRepository<WebsiteSetting>, IWebsiteSettingRepository
    {
        public WebsiteSettingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<WebsiteSetting> GetSettingsAsync()
        {
            var settings = await _dbSet.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new WebsiteSetting { RestaurantName = "Owl Cafe" };
                await AddAsync(settings);
                await SaveChangesAsync();
            }
            return settings;
        }
    }
}
