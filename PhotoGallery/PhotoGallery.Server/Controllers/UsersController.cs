using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Server.Contracts;
using PhotoGallery.Server.Dto;
using PhotoGallery.Server.Entities;

namespace PhotoGallery.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepo;
        public UsersController(IUsersRepository usersRepo) => _usersRepo = usersRepo;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersRepo.GetUsers();

            return Ok(users);
        }

        [HttpGet("{id}", Name = "UsersById")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var users = await _usersRepo.GetUsers(id);
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] UsersForCreationDto users)
        {
            var createdUsers = await _usersRepo.CreateUsers(users);

            return CreatedAtRoute("UsersById", new { id = createdUsers.Id }, createdUsers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers(int id, [FromBody] UsersForUpdateDto users)
        {
            var dbUsers = await _usersRepo.GetUsers(id);
            if (dbUsers == null)
                return NotFound();

            await _usersRepo.UpdateUsers(id, users);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var dbUsers = await _usersRepo.GetUsers(id);
            if (dbUsers == null)
                return NotFound();

            await _usersRepo.DeleteUsers(id);

            return NoContent();
        }
    }
}
