using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Arnis.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("gettingstarted")]
        public IActionResult GettingStarted()
        {
            ViewData["Message"] = "Getting started page.";

            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            ViewData["Message"] = "About page";

            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact page.";

            return View();
        }
    }
}
