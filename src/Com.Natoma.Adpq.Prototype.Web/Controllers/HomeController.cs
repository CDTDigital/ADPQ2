using Microsoft.AspNetCore.Mvc;

namespace Com.Natoma.Adpq.Prototype.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            @ViewData["ApiUrl"] = Startup.ApiUrl;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
