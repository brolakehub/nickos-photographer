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

        public ICollection<AlbumCategory> Categories { get; set; }
    }
}
