using Microsoft.AspNetCore.Mvc;

namespace RentPhoto.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SiteInformation()
        {
            return View();
        }
    }
}
