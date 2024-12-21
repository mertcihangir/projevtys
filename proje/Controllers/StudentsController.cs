using Microsoft.AspNetCore.Mvc;
using Data.Context;
using Data.Entities;
using System.Text.Json;

namespace proje.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult CourseSelectionPage()
        {
            using (Db db = new Db())
            {
                string sessionId = HttpContext?.Session?.GetInt32("CurrentUserId").ToString();
                int currentStudentId = Convert.ToInt32(sessionId);
                Student currentStudent = db.Student.Find(currentStudentId);

                List <Course> allCourses = db.Course.ToList();
                List<dynamic> coursesWithTeachers = new List<dynamic>();

                List<StudentCourses> studentCourses = db.StudentCourses.Where(x=> x.StudentId == currentStudentId).ToList();
                List<dynamic> studentCoursesWithTeacher = new List<dynamic>();

                foreach (Course course in allCourses)
                {
                    TeacherCourses teacherCourses = db.TeacherCourses.Where(x => x.CourseId == course.Id).FirstOrDefault();
                    if (teacherCourses != null)
                    {
                        if (!studentCourses.Any(x=> x.CourseId == course.Id))
                        {
                            Teacher courseTeacher = db.Teacher.Find(teacherCourses.TeacherId);

                            coursesWithTeachers.Add(new
                            {
                                CourseId = course.Id,
                                CourseName = course.Name,
                                CourseCredit = course.Credit,
                                CourseType = course.Type,
                                TeacherName = courseTeacher.Name + " " + courseTeacher.Surname
                            });
                        }
                    }
                }

                foreach (StudentCourses course in studentCourses)
                {
                    Course studentCourse = db.Course.Find(course.CourseId);
                    if (studentCourse != null)
                    {
                        TeacherCourses studentCourseTeacherId = db.TeacherCourses.Where (x => x.CourseId == course.CourseId).FirstOrDefault();
                        if (studentCourseTeacherId != null)
                        {
                            Teacher studentCourseTeacher = db.Teacher.Find(studentCourseTeacherId.TeacherId);
                            studentCoursesWithTeacher.Add(new
                            {
                                CourseId = studentCourse.Id,
                                CourseName = studentCourse.Name,
                                CourseCredit = studentCourse.Credit,
                                CourseType = studentCourse.Type,
                                TeacherName = studentCourseTeacher.Name + " " + studentCourseTeacher.Surname
                            });
                        }
                    }
                }

                return PartialView(new
                {
                    AllCourses = coursesWithTeachers,
                    StudentCourses = studentCoursesWithTeacher,
                    isConfirmed = currentStudent.isConfirmedCourses
                });
            }
        }

        public IActionResult SelectCourses(string SelectedCoursesIds)
        {
            List<int> selectedCoursesIds = JsonSerializer.Deserialize<List<int>>(SelectedCoursesIds);
            using (Db db = new Db())
            {
                List<Course> allCourses = db.Course.ToList();
                List<Course> requiredCourses = new List<Course>();
                foreach (Course course in allCourses)
                {
                    TeacherCourses courseWithTeacher = db.TeacherCourses.Where(x => x.CourseId == course.Id).FirstOrDefault();
                    if (courseWithTeacher != null && course.Type)
                    {
                        requiredCourses.Add(course);
                    }
                }

                bool requiredCoursesSelected = true;
                foreach (Course requiredCourse in requiredCourses)
                {
                    if (!selectedCoursesIds.Contains(requiredCourse.Id))
                    {
                        requiredCoursesSelected = false;
                        break;
                    }
                }

                if (!requiredCoursesSelected)
                {
                    return Json(new { success = false, message = "Tüm zorunlu dersler seçilmek zorunda!" });
                }

                string sessionId = HttpContext?.Session?.GetInt32("CurrentUserId").ToString();
                int currentStudentId = Convert.ToInt32(sessionId);
                Student currentStudent = db.Student.Find(currentStudentId);

                foreach (int selectedCourseId in selectedCoursesIds)
                {
                    StudentCourses studentCourse = new StudentCourses
                    {
                        StudentId = currentStudent.Id,
                        CourseId = selectedCourseId
                    };

                    db.StudentCourses.Add(studentCourse);
                }

                db.SaveChanges();
                return Json(new { success = true, message = "Ders seçiminiz yapıldı, danışman hocanızdan onay bekleniyor!" });
            }
        }

        public IActionResult Transcript()
        {
            using (Db db = new Db())
            {
                string sessionId = HttpContext?.Session?.GetInt32("CurrentUserId").ToString();
                int currentStudentId = Convert.ToInt32(sessionId);
                Student currentStudent = db.Student.Find(currentStudentId);
                List<dynamic> studentCoursesWithTeacher = new List<dynamic>();

                if (currentStudent.isConfirmedCourses)
                {
                    List<StudentCourses> studentCoursesIds = db.StudentCourses.Where(x => x.StudentId == currentStudent.Id).ToList();
                    foreach (StudentCourses studentCoursesId in studentCoursesIds)
                    {
                        Course studentCourse = db.Course.Find(studentCoursesId.CourseId);
                        if (studentCourse != null)
                        {
                            TeacherCourses teacherId = db.TeacherCourses.Where(x => x.CourseId == studentCourse.Id).FirstOrDefault();
                            Teacher courseTeacher = db.Teacher.Find(teacherId.TeacherId);

                            studentCoursesWithTeacher.Add(new
                            {
                                CourseId = studentCourse.Id,
                                CourseName = studentCourse.Name,
                                CourseCredit = studentCourse.Credit,
                                CourseType = studentCourse.Type,
                                TeacherName = courseTeacher.Name + " " + courseTeacher.Surname,
                            });
                        }
                    }
                }

                return PartialView(studentCoursesWithTeacher);
            }
        }
    }
}
