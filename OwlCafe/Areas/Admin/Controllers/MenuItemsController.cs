using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuItemsController : Controller
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<MenuItemsController> _logger;

        public MenuItemsController(
            IMenuItemRepository menuItemRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment environment,
            ILogger<MenuItemsController> logger)
        {
            _menuItemRepository = menuItemRepository;
            _categoryRepository = categoryRepository;
            _environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                const int pageSize = 10;
                var menuItems = await _menuItemRepository.GetMenuItemsWithPaginationAsync(page, pageSize);
                var totalCount = await _menuItemRepository.GetTotalMenuItemsCountAsync();

                ViewData["TotalPages"] = (int)Math.Ceiling((double)totalCount / pageSize);
                ViewData["CurrentPage"] = page;

                return View(menuItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading menu items");
                return View(new List<MenuItem>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var menuItem = await _menuItemRepository.GetQueryable()
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
                return NotFound();

            return View(menuItem);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewData["Categories"] = categories;
            return View(new MenuItem { IsAvailable = true, Status = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem, IFormFile? imageFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var categories = await _categoryRepository.GetAllAsync();
                    ViewData["Categories"] = categories;
                    return View(menuItem);
                }

                menuItem.Image = await ResolveMenuImageAsync(menuItem.Image, imageFile);
                if (!ModelState.IsValid)
                {
                    var categories = await _categoryRepository.GetAllAsync();
                    ViewData["Categories"] = categories;
                    return View(menuItem);
                }

                menuItem.CreatedDate = DateTime.UtcNow;
                menuItem.UpdatedDate = DateTime.UtcNow;

                await _menuItemRepository.AddAsync(menuItem);
                await _menuItemRepository.SaveChangesAsync();

                TempData["Success"] = "Menu item created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating menu item");
                var categories = await _categoryRepository.GetAllAsync();
                ViewData["Categories"] = categories;
                ModelState.AddModelError("", "Error creating menu item");
                return View(menuItem);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
                return NotFound();

            var categories = await _categoryRepository.GetAllAsync();
            ViewData["Categories"] = categories;
            return View("Create", menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItem menuItem, IFormFile? imageFile)
        {
            if (id != menuItem.Id)
                return NotFound();

            try
            {
                if (!ModelState.IsValid)
                {
                    var categories = await _categoryRepository.GetAllAsync();
                    ViewData["Categories"] = categories;
                    return View("Create", menuItem);
                }

                var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
                if (existingMenuItem == null)
                    return NotFound();

                existingMenuItem.CategoryId = menuItem.CategoryId;
                existingMenuItem.Name = menuItem.Name;
                existingMenuItem.Description = menuItem.Description;
                existingMenuItem.Price = menuItem.Price;
                existingMenuItem.DiscountPrice = menuItem.DiscountPrice;
                existingMenuItem.Image = await ResolveMenuImageAsync(menuItem.Image, imageFile);
                if (!ModelState.IsValid)
                {
                    var categories = await _categoryRepository.GetAllAsync();
                    ViewData["Categories"] = categories;
                    return View("Create", menuItem);
                }

                existingMenuItem.IsVeg = menuItem.IsVeg;
                existingMenuItem.IsAvailable = menuItem.IsAvailable;
                existingMenuItem.IsFeatured = menuItem.IsFeatured;
                existingMenuItem.PreparationTime = menuItem.PreparationTime;
                existingMenuItem.DisplayOrder = menuItem.DisplayOrder;
                existingMenuItem.Status = menuItem.Status;
                existingMenuItem.UpdatedDate = DateTime.UtcNow;

                _menuItemRepository.Update(existingMenuItem);
                await _menuItemRepository.SaveChangesAsync();

                TempData["Success"] = "Menu item updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating menu item");
                var categories = await _categoryRepository.GetAllAsync();
                ViewData["Categories"] = categories;
                ModelState.AddModelError("", "Error updating menu item");
                return View("Create", menuItem);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if (menuItem == null)
                    return NotFound();

                _menuItemRepository.Remove(menuItem);
                await _menuItemRepository.SaveChangesAsync();

                TempData["Success"] = "Menu item deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting menu item");
                TempData["Error"] = "Error deleting menu item";
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task<string?> ResolveMenuImageAsync(string? imageUrl, IFormFile? imageFile)
        {
            imageUrl = NormalizeImagePath(imageUrl);

            if (imageFile == null || imageFile.Length == 0)
                return imageUrl;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("Image", "Please upload a JPG, PNG, WEBP, or GIF image.");
                return imageUrl;
            }

            var webRootPath = string.IsNullOrWhiteSpace(_environment.WebRootPath)
                ? Path.Combine(_environment.ContentRootPath, "wwwroot")
                : _environment.WebRootPath;

            var uploadsRoot = Path.Combine(webRootPath, "images", "menu");
            Directory.CreateDirectory(uploadsRoot);

            var fileName = $"{Guid.NewGuid():N}{extension}";
            var filePath = Path.Combine(uploadsRoot, fileName);

            await using var stream = System.IO.File.Create(filePath);
            await imageFile.CopyToAsync(stream);

            return $"/images/menu/{fileName}";
        }

        private static string? NormalizeImagePath(string? imageUrl)
        {
            return string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl.Trim();
        }
    }
}
