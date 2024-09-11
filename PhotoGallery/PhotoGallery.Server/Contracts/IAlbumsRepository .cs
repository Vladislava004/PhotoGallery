using PhotoGallery.Server.Dto;
using PhotoGallery.Server.Entities;

namespace PhotoGallery.Server.Contracts
{
    public interface IAlbumsRepository
    {
        public Task<IEnumerable<Albums>> GetAlbums();
        public Task<Albums> GetAlbums(int id);
        public Task<Albums> CreateAlbums(AlbumsForCreationDto albums);
        public Task UpdateAlbums(int id, AlbumsForUpdateDto albums);
        public Task DeleteAlbums(int id);
    }
}
