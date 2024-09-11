using PhotoGallery.Server.Dto;
using PhotoGallery.Server.Entities;

namespace PhotoGallery.Server.Contracts
{
    public interface IPhotosRepository
    {
        public Task<IEnumerable<Photos>> GetPhotos();
        public Task<Photos> GetPhotos(int id);
        public Task<Photos> CreatePhotos(PhotosForCreationDto photos);
        public Task UpdatePhotos(int id, PhotosForUpdateDto photos);
        public Task DeletePhotos(int id);
    }
}
