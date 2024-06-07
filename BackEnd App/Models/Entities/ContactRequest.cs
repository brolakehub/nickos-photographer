using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_App.Models.Entities
{
    [Table("contactRequests", Schema = "data")]
    public class ContactRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Subject { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Message { get; set; }
    }
}
