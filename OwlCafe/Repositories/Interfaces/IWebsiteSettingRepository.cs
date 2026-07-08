using OwlCafe.Models;

namespace OwlCafe.Repositories.Interfaces
{
    public interface IWebsiteSettingRepository : IGenericRepository<WebsiteSetting>
    {
        Task<WebsiteSetting> GetSettingsAsync();
    }
}
