namespace DTO
{
    public class AlbumCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
    }
}
