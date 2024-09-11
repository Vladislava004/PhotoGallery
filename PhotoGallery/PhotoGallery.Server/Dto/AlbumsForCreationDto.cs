namespace PhotoGallery.Server.Dto
{
    public class AlbumsForCreationDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
