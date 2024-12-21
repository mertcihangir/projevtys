using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class StudentCourses
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
