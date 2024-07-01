namespace DTO
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AlbumCategory> Categories { get; set; }
        public List<File> Files { get; set; }
    }
}
