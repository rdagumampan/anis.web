using Microsoft.AspNet.Mvc;

namespace Arnis.Web.Controllers
{
    public class StatisticsController : Controller
    {
        [Route("statistics")]
        public IActionResult GetStatistics(string userName, string workspaceName)
        {
            return View();
        }
    }
}