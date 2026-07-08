using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GalleryController : Controller
    {
        private readonly IGalleryImageRepository _galleryRepository;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IGalleryImageRepository galleryRepository, ILogger<GalleryController> logger)
        {
            _galleryRepository = galleryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var images = await _galleryRepository.GetAllAsync();
                return View(images.OrderByDescending(g => g.CreatedDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading gallery");
                return View(new List<GalleryImage>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GalleryImage galleryImage)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(galleryImage);

                galleryImage.CreatedDate = DateTime.UtcNow;

                await _galleryRepository.AddAsync(galleryImage);
                await _galleryRepository.SaveChangesAsync();

                TempData["Success"] = "Gallery image added successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating gallery image");
                ModelState.AddModelError("", "Error creating gallery image");
                return View(galleryImage);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var image = await _galleryRepository.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            return View("Create", image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GalleryImage galleryImage)
        {
            if (id != galleryImage.Id)
                return NotFound();

            try
            {
                var existingImage = await _galleryRepository.GetByIdAsync(id);
                if (existingImage == null)
                    return NotFound();

                existingImage.Title = galleryImage.Title;
                existingImage.Image = galleryImage.Image;
                existingImage.Category = galleryImage.Category;
                existingImage.DisplayOrder = galleryImage.DisplayOrder;
                existingImage.Status = galleryImage.Status;

                _galleryRepository.Update(existingImage);
                await _galleryRepository.SaveChangesAsync();

                TempData["Success"] = "Gallery image updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating gallery image");
                ModelState.AddModelError("", "Error updating gallery image");
                return View("Create", galleryImage);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var image = await _galleryRepository.GetByIdAsync(id);
                if (image == null)
                    return NotFound();

                _galleryRepository.Remove(image);
                await _galleryRepository.SaveChangesAsync();

                TempData["Success"] = "Gallery image deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting gallery image");
                TempData["Error"] = "Error deleting gallery image";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
