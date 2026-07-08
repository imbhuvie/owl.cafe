using Microsoft.AspNetCore.Mvc;
using OwlCafe.Data;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebsiteSettingRepository _websiteSettingRepository;

        public FooterViewComponent(ApplicationDbContext context, IWebsiteSettingRepository websiteSettingRepository)
        {
            _context = context;
            _websiteSettingRepository = websiteSettingRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _websiteSettingRepository.GetSettingsAsync();

            return View(settings);
        }
    }
}
