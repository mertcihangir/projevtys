using Microsoft.AspNetCore.Mvc;
using Data.Context;
using Data.Entities;

namespace proje.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Entering(string email, string password)
        {
            using (Db db = new Db()) 
            {
                List<Student> isStudent = db.Student.Where(x => x.Email == email && x.Password == password).ToList();
                List<Teacher> isTeacher = db.Teacher.Where(x => x.Email == email && x.Password == password).ToList();
                if (isStudent.Count > 0)
                {
                    Student currentUser = isStudent[0];
                    HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);
                    HttpContext?.Session?.SetString("Job", currentUser.Role);
                    HttpContext?.Session?.SetInt32("CurrentUserId", currentUser.Id);
                    HttpContext?.Session?.SetString("Role", "Student");

                    return RedirectToAction("Index", "Home");
                }

                if (isTeacher.Count > 0)
                {
                    Teacher currentUser = isTeacher[0];
                    HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);
                    HttpContext?.Session?.SetString("Job", currentUser.Role);
                    HttpContext?.Session?.SetString("Role", "Teacher");
                    HttpContext?.Session?.SetInt32("CurrentUserId", currentUser.Id);

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
