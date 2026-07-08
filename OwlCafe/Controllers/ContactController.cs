using Microsoft.AspNetCore.Mvc;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IWebsiteSettingRepository _websiteSettingRepository;
        private readonly ILogger<ContactController> _logger;

        public ContactController(
            IContactMessageRepository contactMessageRepository,
            IWebsiteSettingRepository websiteSettingRepository,
            ILogger<ContactController> logger)
        {
            _contactMessageRepository = contactMessageRepository;
            _websiteSettingRepository = websiteSettingRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var settings = await _websiteSettingRepository.GetSettingsAsync();
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactMessage contactMessage)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(contactMessage);

                contactMessage.CreatedDate = DateTime.UtcNow;
                contactMessage.IsRead = false;

                await _contactMessageRepository.AddAsync(contactMessage);
                await _contactMessageRepository.SaveChangesAsync();

                _logger.LogInformation($"New contact message received from {contactMessage.Email}");
                TempData["Success"] = "Thank you for your message! We will get back to you soon.";
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending contact message");
                ModelState.AddModelError("", "An error occurred while sending your message. Please try again.");
                return View(contactMessage);
            }
        }
    }
}
