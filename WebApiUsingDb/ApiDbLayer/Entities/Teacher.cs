using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDbLayer.Entities
{
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TeacherId { get; set; }
        [Column(TypeName = "Varchar(50)")]
        public string? TeacherName { get; set; }
        [Column(TypeName = "Varchar(50)")]
        public string? TeacherAddress { get; set; }

        public ICollection<Classroom>? classrooms { get; set; }
    }
}
