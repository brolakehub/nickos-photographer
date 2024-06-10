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

        public DTO.ContactRequest ToDTOContactRequest() =>
            new()
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Subject = Subject,
                Message = Message,
            };

        public ContactRequest FromDTOContactRequest(DTO.ContactRequest contactRequest)
        {
            Name = contactRequest.Name;
            Email = contactRequest.Email;
            Phone = contactRequest.Phone;
            Subject = contactRequest.Subject;
            Message = contactRequest.Message;

            return this;
        }
    }
}
