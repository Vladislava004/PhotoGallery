using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Server.Contracts;
using PhotoGallery.Server.Dto;
using PhotoGallery.Server.Entities;

namespace PhotoGallery.Server.Controllers
{
    [Route("api/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotosRepository _photosRepo;
        public PhotosController(IPhotosRepository photosRepo) => _photosRepo = photosRepo;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var photos = await _photosRepo.GetPhotos();

            return Ok(photos);
        }

        [HttpGet("{id}", Name = "PhotosById")]
        public async Task<IActionResult> GetPhotos(int id)
        {
            var photos = _photosRepo.GetPhotos(id);
            if (photos == null)
                return NotFound();

            return Ok(photos);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhotos([FromBody] PhotosForCreationDto photos)
        {
            var createdPhotos = await _photosRepo.CreatePhotos(photos);

            return CreatedAtRoute("PhotosById", new { id = createdPhotos.Id }, createdPhotos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhotos(int id, [FromBody] PhotosForUpdateDto photos)
        {
            var dbPhotos = await _photosRepo.GetPhotos(id);
            if (dbPhotos == null)
                return NotFound();

            await _photosRepo.UpdatePhotos(id, photos);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhotos(int id)
        {
            var dbPhotos = await _photosRepo.GetPhotos(id);
            if (dbPhotos == null)
                return NotFound();

            await _photosRepo.DeletePhotos(id);

            return NoContent();
        }
    }
}
