using Dapper;
using PhotoGallery.Server.Contracts;
using PhotoGallery.Server.Context;
using PhotoGallery.Server.Entities;
using PhotoGallery.Server.Dto;
using System.Data;

namespace PhotoGallery.Server.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private DapperContext _context;
        public UsersRepository(DapperContext context) => _context = context;

        public async Task<Users> CreateUsers(UsersForCreationDto users)
        {
            var query = "INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password)" + 
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", users.Name, DbType.String);
            parameters.Add("Email", users.Email, DbType.String);
            parameters.Add("Password", users.Password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
               var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdUsers = new Users
                {
                    Id = id,
                    Name = users.Name,
                    Email = users.Email,
                    Password = users.Password,
                };

                return createdUsers;
            }
        }

        public async Task DeleteUsers(int id)
        {
            var query = "DELETE FROM Users WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            var query = "SELECT * FROM Users";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<Users>(query);

                return users.ToList();
            }
        }

        public async Task<Users> GetUsers(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QuerySingleOrDefaultAsync<Users>(query, new { id });

                return users;
            }
        }

        public async Task UpdateUsers(int id, UsersForUpdateDto users)
        {
            var query = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", users.Name, DbType.String);
            parameters.Add("Email", users.Email, DbType.String);
            parameters.Add("Password", users.Password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }

}
