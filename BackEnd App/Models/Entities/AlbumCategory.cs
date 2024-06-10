using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_App.Models.Entities
{
    [Table("albumCategories", Schema = "filter")]
    public class AlbumCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; } = new List<Album>();

        public DTO.AlbumCategory ToDTOAlbumCategory() =>
            new()
            {
                Id = Id,
                Name = Name,
                Albums = Albums.ToList().Select(f => f.ToDTOAlbum()).ToList(),
            };
    }
}
