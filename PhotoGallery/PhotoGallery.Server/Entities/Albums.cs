namespace PhotoGallery.Server.Entities
{
    public class Albums
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set;}
    }
}
