using Microsoft.AspNet.Mvc;

namespace Arnis.Web.Controllers
{
    public class AccountsController : Controller
    {
        [Route("{userName}")]
        public IActionResult GetByUserName(string userName)
        {
            return View();
        }
    }
}