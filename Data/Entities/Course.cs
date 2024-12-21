using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Credit { get; set; }

        public bool Type { get; set; } = false;
    }
}
