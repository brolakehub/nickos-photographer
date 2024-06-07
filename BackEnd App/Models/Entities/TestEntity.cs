using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_App.Models.Entities
{
    public class TestEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(MAX)")]
        public string Text { get; set; }
    }
}
