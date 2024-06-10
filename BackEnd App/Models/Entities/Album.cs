using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_App.Models.Entities
{
    [Table("albums", Schema = "filter")]
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; }

        public ICollection<AlbumCategory> Categories { get; set; } = new List<AlbumCategory>();

        public ICollection<File> Files { get; set; } = new List<File>();

        public DTO.Album ToDTOAlbum() =>
            new()
            {
                Id = Id,
                Name = Name,
                Categories = Categories.ToList().Select(c => c.ToDTOAlbumCategory()).ToList(),
                Files = Files.ToList().Select(f => f.ToDTOFile()).ToList(),
            };
    }
}
