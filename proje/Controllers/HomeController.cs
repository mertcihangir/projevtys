using System.Diagnostics;
using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using proje.Models;

namespace proje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MyProfile()
        {
            using (Db db = new Db())
            {
                int currentUserId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserId"));
                Student currentUser = db.Student.Find(currentUserId);

                return PartialView(currentUser);
            }

        }

        [HttpPost]
        public IActionResult UpdateProfile(string Name, string Surname, string Email, string Password)
        {

            if (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Surname) || !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Password))
            {
                using (Db db = new Db())
                {
                    int currentUserId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserId"));
                    Student currentUser = db.Student.Find(currentUserId);

                    currentUser.Name = Name;
                    currentUser.Surname = Surname;
                    currentUser.Email = Email;
                    currentUser.Password= Password;

                    db.SaveChanges();
                    HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);

                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
