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
                List<Admin> isAdmin = db.Admin.Where(x => x.Email == email && x.Password == password).ToList();
                if (isAdmin.Count > 0)
                {
                    Admin currentUser = isAdmin[0];
                    HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);
                    HttpContext?.Session?.SetString("Job", currentUser.Role);
                    HttpContext?.Session?.SetInt32("CurrentUserId", currentUser.Id);



                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
