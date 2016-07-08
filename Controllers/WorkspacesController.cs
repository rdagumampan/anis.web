using Arnis.Web.Repositiories;
using Microsoft.AspNet.Mvc;

namespace Arnis.Web.Controllers
{
    public class WorkspacesController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkspacesController(
            IAccountRepository accountRepository,
            IWorkspaceRepository workspaceRepository)
        {
            _accountRepository = accountRepository;
            _workspaceRepository = workspaceRepository;
        }

        [Route("workspaces")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{userName}/{workspaceName}")]
        public IActionResult GetWorkspace(string userName, string workspaceName)
        {
            //get the account
            var account = _accountRepository.GetByUserName(userName);
            if (null != account)
            {
                //get the workspace
                var workspace = _workspaceRepository.GetByName(account.Id, workspaceName);
                if (null != workspace)
                {
                    return View(workspace);
                }
                else
                {
                    //do something here
                    return View();
                }
            }
            else
            {
                //do something here
                return View();
            }
        }
    }
}