using Microsoft.AspNetCore.Mvc;

namespace proje.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
