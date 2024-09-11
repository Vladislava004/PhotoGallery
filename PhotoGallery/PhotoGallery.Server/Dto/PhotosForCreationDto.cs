namespace PhotoGallery.Server.Dto
{
    public class PhotosForCreationDto
    {
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime? UploadedOn { get; set; }
    }
}
