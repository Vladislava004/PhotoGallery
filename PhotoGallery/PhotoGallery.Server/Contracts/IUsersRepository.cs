using PhotoGallery.Server.Dto;
using PhotoGallery.Server.Entities;

namespace PhotoGallery.Server.Contracts
{
    public interface IUsersRepository
    {
        public Task<IEnumerable<Users>> GetUsers();
        public Task<Users> GetUsers(int id);
        public Task<Users> CreateUsers(UsersForCreationDto users);
        public Task UpdateUsers(int id, UsersForUpdateDto users);
        public Task DeleteUsers(int id);
    }
}
