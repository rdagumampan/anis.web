using Microsoft.AspNet.Mvc;

namespace Arnis.Web.Controllers
{
    public class WorkspacesController : Controller
    {
        [Route("workspaces")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{userName}/{workspaceName}")]
        public IActionResult GetWorkspaceByName(string userName, string workspaceName)
        {
            return View();
        }
    }
}