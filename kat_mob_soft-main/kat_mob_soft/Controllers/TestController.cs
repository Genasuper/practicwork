using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return Content("Тест работает! MVC настроен правильно.");
        }
    }
}