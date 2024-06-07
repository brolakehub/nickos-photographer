using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_App.Models.Entities
{
    [Table("infos", Schema = "deatails")]
    public class Info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Value { get; set; }
    }
}
