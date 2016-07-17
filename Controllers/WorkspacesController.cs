using Arnis.Web.Repositiories;
using Microsoft.AspNetCore.Mvc;
using Arnis.Web.Models;
using System.Linq;
using System.Collections.Generic;

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
                    var solutionDependencies = workspace.Solutions
                        .SelectMany(s => s.Dependencies
                        .Select(sd => new
                        {
                            Name = sd.Name,
                            Version = sd.Version,
                            Solution = s.Name,
                            Project = string.Empty,
                            SolutionFile = s.Location,
                            ProjectFile = string.Empty,
                        }));

                    var projectDependencies = workspace.Solutions
                        .SelectMany(s => s.Projects
                            .SelectMany(p => p.Dependencies
                                .Select(pd => new
                                {
                                    Name = pd.Name,
                                    Version = pd.Version,
                                    Solution = s.Name,
                                    Project = p.Name,
                                    SolutionFile = s.Location,
                                    ProjectFile = p.Location,
                                })));

                    var dependencies = new List<WorkspaceDependencyVm>();
                    solutionDependencies
                       .Union(projectDependencies)
                       .GroupBy(r => r.Name)
                       .ToList()
                       .ForEach(g =>
                       {
                           g.ToList()
                           .OrderBy(r => r.Version)
                           .GroupBy(grpv => grpv.Version)
                           .ToList()
                           .ForEach(gv =>
                           {
                               gv.ToList()
                                   .ForEach(r =>
                                   {
                                       dependencies.Add(new WorkspaceDependencyVm
                                       {
                                           Name = r.Name,
                                           Version = r.Version,
                                           Solution = r.Solution,
                                           Project = r.Project,
                                           SolutionFile = r.SolutionFile,
                                           ProjectFile = r.ProjectFile
                                       });
                                   });
                           });
                       });

                    var workspaceVm = new WorkspaceVm
                    {
                        Name = workspace.Name,
                        Description = workspace.Description,
                        Owners = workspace.Owners,
                        Logs = workspace.Logs.Select(l => new LogVm { Message = l }).ToList(),
                        Dependencies = dependencies.ToList()
                    };

                    return View(workspaceVm);
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