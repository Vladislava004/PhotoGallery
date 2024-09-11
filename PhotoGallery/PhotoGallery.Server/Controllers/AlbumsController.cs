using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Server.Contracts;
using PhotoGallery.Server.Dto;
using PhotoGallery.Server.Entities;

namespace PhotoGallery.Server.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsRepository _albumsRepo;
        public AlbumsController(IAlbumsRepository albumsRepo) => _albumsRepo = albumsRepo;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var albums = await _albumsRepo.GetAlbums();

            return Ok(albums);
        }

        [HttpGet("{id}", Name = "AlbumsById")]
        public async Task<IActionResult> GetAlbums(int id)
        {
            var albums = _albumsRepo.GetAlbums(id);
            if (albums == null)
                return NotFound();

            return Ok(albums);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbums([FromBody] AlbumsForCreationDto albums)
        {
            var createdAlbums = await _albumsRepo.CreateAlbums(albums);

            return CreatedAtRoute("AlbumsById", new { id = createdAlbums.Id }, createdAlbums);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbums(int id, [FromBody] AlbumsForUpdateDto albums)
        {
            var dbAlbums = await _albumsRepo.GetAlbums(id);
            if (dbAlbums == null)
                return NotFound();

            await _albumsRepo.UpdateAlbums(id, albums);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbums(int id)
        {
            var dbAlbums = await _albumsRepo.GetAlbums(id);
            if (dbAlbums == null)
                return NotFound();

            await _albumsRepo.DeleteAlbums(id);

            return NoContent();
        }
    }
}
