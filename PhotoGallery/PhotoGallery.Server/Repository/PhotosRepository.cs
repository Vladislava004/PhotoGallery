using Dapper;
using PhotoGallery.Server.Contracts;
using PhotoGallery.Server.Context;
using PhotoGallery.Server.Entities;
using PhotoGallery.Server.Dto;
using System.Data;

namespace PhotoGallery.Server.Repository
{
    public class PhotosRepository : IPhotosRepository
    {
        private DapperContext _context;
        public PhotosRepository(DapperContext context) => _context = context;

        public async Task<Photos> CreatePhotos(PhotosForCreationDto photos)
        {
            var query = "INSERT INTO Photos (UserId, AlbumId, PhotoUrl, UploadedOn) VALUES (@UserId, @AlbumId, @PhotoUrl, @UploadedOn)" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", photos.UserId, DbType.Int32);
            parameters.Add("AlbumId", photos.AlbumId, DbType.Int32);
            parameters.Add("PhotoUrl", photos.PhotoUrl, DbType.String);
            parameters.Add("UploadedOn", photos.UploadedOn, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdPhotos = new Photos
                {
                    Id = id,
                    UserId = photos.UserId,
                    AlbumId = photos.AlbumId,
                    PhotoUrl = photos.PhotoUrl,
                    UploadedOn = photos.UploadedOn,
                };

                return createdPhotos;
            }
        }

        public async Task DeletePhotos(int id)
        {
            var query = "DELETE FROM Photos WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Photos>> GetPhotos()
        {
            var query = "SELECT * FROM Photos";

            using (var connection = _context.CreateConnection())
            {
                var photos = await connection.QueryAsync<Photos>(query);

                return photos.ToList();
            }
        }

        public async Task<Photos> GetPhotos(int id)
        {
            var query = "SELECT * FROM Photos WHERE Id = @Id";

            using (var connection = _context.CreateConnection()) 
            {
                var photos = await connection.QuerySingleOrDefaultAsync<Photos>(query, new { id });

                return photos;
            }
        }

        public async Task UpdatePhotos(int id, PhotosForUpdateDto photos)
        {
            var query = "UPDATE Photos SET UserId = @UserId, AlbumId = @AlbumId, PhotoUrl = @PhotoUrl, UploadedOn = @UploadedOn WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("UserId", photos.UserId, DbType.Int32);
            parameters.Add("AlbumId", photos.AlbumId, DbType.Int32);
            parameters.Add("PhotoUrl", photos.PhotoUrl, DbType.String);
            parameters.Add("UploadedOn", photos.UploadedOn, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }

}
