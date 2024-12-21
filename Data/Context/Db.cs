
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Data.Context
{
    public class Db : DbContext
    {
        public Db() : base(options: new DbContextOptionsBuilder<Db>().UseSqlServer("Data Source=DESKTOP-D516960\\MSSQLSERVER01;Initial Catalog=proje;Integrated Security=True;Encrypt=False").Options)
        {
        }

        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<TeacherCourses> TeacherCourses { get; set; }
        public virtual DbSet<StudentCourses> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(c => c.Type)
                .HasDefaultValue(false);

            modelBuilder.Entity<Student>()
                .Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETDATE()");


            base.OnModelCreating(modelBuilder);
        }
    }
}