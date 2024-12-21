using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedDate { get; set; }

        public int InstructorId { get; set; }

        public bool isConfirmedCourses { get; set; } = false;

        [NotMapped]
        public string Role = "Öğrenci";
    }
}
