namespace PhotoGallery.Server.Dto
{
    public class AlbumsForUpdateDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
