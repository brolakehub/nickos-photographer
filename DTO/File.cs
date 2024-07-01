namespace DTO
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Album> Albums { get; set; }
        public FileType Type { get; set; }
    }
}
