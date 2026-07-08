using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantTableRepository _tableRepository;
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            IBookingRepository bookingRepository,
            ICategoryRepository categoryRepository,
            IMenuItemRepository menuItemRepository,
            IRestaurantTableRepository tableRepository,
            IContactMessageRepository contactMessageRepository,
            ILogger<DashboardController> logger)
        {
            _bookingRepository = bookingRepository;
            _categoryRepository = categoryRepository;
            _menuItemRepository = menuItemRepository;
            _tableRepository = tableRepository;
            _contactMessageRepository = contactMessageRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var todayBookings = await _bookingRepository.GetTodayBookingsAsync();
                var totalBookings = await _bookingRepository.GetAllAsync();
                var menuItems = await _menuItemRepository.GetAllAsync();
                var categories = await _categoryRepository.GetAllAsync();
                var tables = await _tableRepository.GetAllAsync();
                var unreadMessages = await _contactMessageRepository.GetTotalUnreadCountAsync();

                ViewData["TodayBookings"] = todayBookings.Count();
                ViewData["TotalBookings"] = totalBookings.Count();
                ViewData["MenuItems"] = menuItems.Count();
                ViewData["Categories"] = categories.Count();
                ViewData["Tables"] = tables.Count();
                ViewData["UnreadMessages"] = unreadMessages;
                ViewData["RecentBookings"] = todayBookings.Take(5);

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard");
                return View();
            }
        }
    }
}
