using Microsoft.AspNetCore.Mvc;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryImageRepository _galleryImageRepository;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IGalleryImageRepository galleryImageRepository, ILogger<GalleryController> logger)
        {
            _galleryImageRepository = galleryImageRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? category = null)
        {
            try
            {
                var images = category != null
                    ? await _galleryImageRepository.GetGalleryImagesByCategoryAsync(category)
                    : await _galleryImageRepository.GetActiveGalleryImagesAsync();

                return View(images);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading gallery");
                return View(new List<object>());
            }
        }
    }
}
