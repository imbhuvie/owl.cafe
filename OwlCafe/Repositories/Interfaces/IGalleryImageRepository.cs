using OwlCafe.Models;

namespace OwlCafe.Repositories.Interfaces
{
    public interface IGalleryImageRepository : IGenericRepository<GalleryImage>
    {
        Task<IEnumerable<GalleryImage>> GetActiveGalleryImagesAsync();
        Task<IEnumerable<GalleryImage>> GetGalleryImagesByCategoryAsync(string category);
    }
}
