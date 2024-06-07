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
    }
}
