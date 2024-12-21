using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using Data.Context;

namespace proje.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult CourseConfirmPage()
        {
            using (Db db = new Db())
            {
                string sessionId = HttpContext?.Session?.GetInt32("CurrentUserId").ToString();
                int currentTeacherId = Convert.ToInt32(sessionId);

                List<Student> students = db.Student.Where(x=> x.InstructorId == currentTeacherId && x.isConfirmedCourses == false).ToList();
                return PartialView(students);
            }
        }

        [HttpPost]
        public IActionResult ConfirmCourses(int Id)
        {
            using (Db db = new Db())
            {
                Student student = db.Student.Find(Id);
                if (student == null)
                {
                    return Json(new { success = false, message = "İşlem Başarısız! Bir sorun oluştu!" });
                }

                student.isConfirmedCourses = true;
                db.SaveChanges();

                return Json(new { success = true, message = "İşlem Başarılı!" });
            }
        }

        [HttpPost]
        public IActionResult RejectCourses(int Id)
        {
            using (Db db = new Db())
            {
                Student student = db.Student.Find(Id);
                if (student == null)
                {
                    return Json(new { success = false, message = "İşlem Başarısız! Bir sorun oluştu!" });
                }

                List<StudentCourses> studentSelectedCourses = db.StudentCourses.Where(x => x.StudentId == Id).ToList();
                foreach (StudentCourses studentSelectedCourse in studentSelectedCourses)
                {
                    db.StudentCourses.Remove(studentSelectedCourse);
                }
                student.isConfirmedCourses = false;
                db.SaveChanges();

                return Json(new { success = true, message = "İşlem Başarılı!" });
            }
        }
    }
}
