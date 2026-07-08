using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly IWebsiteSettingRepository _settingRepository;
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(IWebsiteSettingRepository settingRepository, ILogger<SettingsController> logger)
        {
            _settingRepository = settingRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var settings = await _settingRepository.GetSettingsAsync();
                return View(settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading settings");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(WebsiteSetting setting)
        {
            try
            {
                var existingSetting = await _settingRepository.GetSettingsAsync();

                existingSetting.RestaurantName = setting.RestaurantName;
                existingSetting.Logo = setting.Logo;
                existingSetting.Phone = setting.Phone;
                existingSetting.Email = setting.Email;
                existingSetting.Address = setting.Address;
                existingSetting.OpeningHours = setting.OpeningHours;
                existingSetting.Facebook = setting.Facebook;
                existingSetting.Instagram = setting.Instagram;
                existingSetting.WhatsApp = setting.WhatsApp;
                existingSetting.GoogleMap = setting.GoogleMap;
                existingSetting.SEODescription = setting.SEODescription;
                existingSetting.SEOKeywords = setting.SEOKeywords;
                existingSetting.UpdatedDate = DateTime.UtcNow;

                _settingRepository.Update(existingSetting);
                await _settingRepository.SaveChangesAsync();

                TempData["Success"] = "Settings updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating settings");
                ModelState.AddModelError("", "Error updating settings");
                return View(setting);
            }
        }
    }
}
