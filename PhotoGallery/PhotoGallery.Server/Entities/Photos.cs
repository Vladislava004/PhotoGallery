namespace PhotoGallery.Server.Entities
{
    public class Photos
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime? UploadedOn { get; set; }

    }
}
