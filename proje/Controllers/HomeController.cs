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

        public IActionResult Privacy()
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
                Admin currentUser = db.Admin.Find(currentUserId);

                return PartialView(currentUser);
            }

        }

        public IActionResult L

        public IActionResult GetAllAdmins()
        {
            using (Db db = new Db())
            {
                List<Admin> allAdmins = db.Admin.ToList();
                return PartialView(allAdmins);
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
                    Admin currentUser = db.Admin.Find(currentUserId);

                    currentUser.Name = Name;
                    currentUser.Surname = Surname;
                    currentUser.Email = Email;
                    currentUser.Password= Password;


                    // VERÝ EKLEME KISMI
                    //Student newUser = new Student
                    //{
                    //    Name = "akjhdaskd",
                    //    Surname = "asdsad"
                    //};

                    //db.Student.Add(newUser);


                    db.SaveChanges();
                    HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);

                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
