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

        public File? File { get; set; }

        public DTO.Service ToDTOService() =>
            new()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                File = File?.ToDTOFile()
            };

        public Service FromDTOService(DTO.Service service)
        {
            Title = service.Title;
            Description = service.Description;
            if (service.File != null)
                File = File?.FromDTOFile(service.File);
            return this;
        }
    }
}
