using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDbLayer.Entities
{
    public class Classroom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ClassId { get; set; }
        [Column(TypeName = "Varchar(50)")]
        public string? Name { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
