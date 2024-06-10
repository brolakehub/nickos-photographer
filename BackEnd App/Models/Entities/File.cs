using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_App.Models.Entities
{
    [Table("files", Schema = "data")]
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Path { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public DTO.FileType Type { get; set; }

        public ICollection<Album> Albums { get; set; } = new List<Album>();

        public DTO.File ToDTOFile(bool isRecursive = false) =>
            new()
            {
                Id = Id,
                Name = Name,
                Path = Path,
                Albums = isRecursive
                    ? new List<DTO.Album>()
                    : Albums.ToList().Select(a => a.ToDTOAlbum(true)).ToList(),
                Type = Type
            };

        public File FromDTOFile(DTO.File file)
        {
            Name = file.Name;
            Path = file.Path;
            Type = file.Type;

            return this;
        }
    }
}
