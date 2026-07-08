using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactMessagesController : Controller
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly ILogger<ContactMessagesController> _logger;

        public ContactMessagesController(IContactMessageRepository contactMessageRepository, ILogger<ContactMessagesController> logger)
        {
            _contactMessageRepository = contactMessageRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                const int pageSize = 10;
                var messages = await _contactMessageRepository.GetMessagesPaginatedAsync(page, pageSize);
                
                ViewData["TotalMessages"] = await _contactMessageRepository.GetTotalUnreadCountAsync();
                ViewData["CurrentPage"] = page;

                return View(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading messages");
                return View(new List<ContactMessage>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var message = await _contactMessageRepository.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            if (!message.IsRead)
            {
                message.IsRead = true;
                _contactMessageRepository.Update(message);
                await _contactMessageRepository.SaveChangesAsync();
            }

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            try
            {
                var message = await _contactMessageRepository.GetByIdAsync(id);
                if (message == null)
                    return NotFound();

                message.IsRead = true;
                _contactMessageRepository.Update(message);
                await _contactMessageRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking message as read");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var message = await _contactMessageRepository.GetByIdAsync(id);
                if (message == null)
                    return NotFound();

                _contactMessageRepository.Remove(message);
                await _contactMessageRepository.SaveChangesAsync();

                TempData["Success"] = "Message deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting message");
                TempData["Error"] = "Error deleting message";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
