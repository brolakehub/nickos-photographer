using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_App.Models.Entities
{
    [Table("albumCategorys", Schema = "filter")]
    public class AlbumCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
