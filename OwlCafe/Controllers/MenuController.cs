using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<MenuController> _logger;

        public MenuController(IMenuItemRepository menuItemRepository, ICategoryRepository categoryRepository, ILogger<MenuController> logger)
        {
            _menuItemRepository = menuItemRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? categoryId = null)
        {
            try
            {
                var categories = await _categoryRepository.GetActiveCategoriesAsync();
                var menuItems = categoryId.HasValue
                    ? await _menuItemRepository.GetMenuItemsByCategoryAsync(categoryId.Value)
                    : await _menuItemRepository.GetAvailableMenuItemsAsync();

                ViewData["Categories"] = categories;
                ViewData["SelectedCategoryId"] = categoryId;
                return View(menuItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading menu");
                return View(new List<object>());
            }
        }

        [HttpGet("Menu/Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetQueryable()
                    .Include(m => m.Category)
                    .FirstOrDefaultAsync(m => m.Id == id && m.Status);
                if (menuItem == null)
                    return NotFound();

                return View(menuItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading menu item details");
                return NotFound();
            }
        }
    }
}
