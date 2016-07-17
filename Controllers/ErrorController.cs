using Microsoft.AspNetCore.Mvc;

namespace Arnis.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("oops")]
        public IActionResult Index()
        {
            return View("Error");
        }
    }
}