using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.Web.Controllers
{
    public class SimpleEquipmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}