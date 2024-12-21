using Microsoft.AspNetCore.Mvc;

namespace proje.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
