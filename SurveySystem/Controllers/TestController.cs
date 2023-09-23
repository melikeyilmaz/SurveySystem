using Microsoft.AspNetCore.Mvc;

namespace SurveySystem.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
