using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class TeacherCourses
    {
        [Key]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

    }
}
