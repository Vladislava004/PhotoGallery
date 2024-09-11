using Dapper;
using PhotoGallery.Server.Contracts;
using PhotoGallery.Server.Context;
using PhotoGallery.Server.Entities;
using PhotoGallery.Server.Dto;
using System.Data;

namespace PhotoGallery.Server.Repository
{
    public class AlbumsRepository : IAlbumsRepository 
    {
        private DapperContext _context;
        public AlbumsRepository(DapperContext context) => _context = context;

        public async Task<Albums> CreateAlbums(AlbumsForCreationDto albums)
        {
            var query = "INSERT INTO Photos (UserId, Name, CreatedDate) VALUES (@UserId, @Name, @CreatedDate)" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", albums.UserId, DbType.Int32);
            parameters.Add("Name", albums.Name, DbType.String);
            parameters.Add("CreatedDate", albums.CreatedDate, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdAlbums = new Albums
                {
                    Id = id,
                    UserId = albums.UserId,
                    Name = albums.Name,
                    CreatedDate = albums.CreatedDate,
                };

                return createdAlbums;
            }
        }

        public async Task DeleteAlbums(int id)
        {
            var query = "DELETE FROM Albums WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Albums>> GetAlbums()
        {
            var query = "SELECT * FROM Albums";

            using (var connection = _context.CreateConnection())
            {
                var albums = await connection.QueryAsync<Albums>(query);

                return albums.ToList();
            }
        }

        public async Task<Albums> GetAlbums(int id)
        {
            var query = "SELECT * FROM Albums WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var albums = await connection.QuerySingleOrDefaultAsync<Albums>(query, new { id });

                return albums;
            }
        }

        public async Task UpdateAlbums(int id, AlbumsForUpdateDto albums)
        {
            var query = "UPDATE Albums SET UserId = @UserId, Name = @Name, CreatedDate = @CreatedDate WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("UserId", albums.UserId, DbType.Int32);
            parameters.Add("Name", albums.Name, DbType.String);
            parameters.Add("CreatedDate", albums.CreatedDate, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
