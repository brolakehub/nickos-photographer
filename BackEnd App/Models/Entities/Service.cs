using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_App.Models.Entities
{
    [Table("services", Schema = "deatails")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Field { get; set; }
    }
}
