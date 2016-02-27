using System.Collections.Generic;
using Arnis.Web.ApiModels;
using Arnis.Web.Models;
using Arnis.Web.Repositiories;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;

namespace Arnis.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IAccountRepository _accountRepository;

        public WorkspaceController(IWorkspaceRepository workspaceRepository, IAccountRepository accountRepository)
        {
            _workspaceRepository = workspaceRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IEnumerable<Workspace> GetAll()
        {
            var workspaces = _workspaceRepository.All();
            return workspaces;
        }

        [HttpGet("{id:length(24)}", Name = "GetByIdRoute")]
        public IActionResult GetById(string id)
        {
            var item = _workspaceRepository.GetById(new ObjectId(id));
            if (item == null)
            {
                return HttpNotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public void Create([FromBody] WorkspaceRequest request)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;  //400 Bad Request
            }
            else
            {
                //validate api key
                var account = _accountRepository.GetByApiKey(request.ApiKey);
                if(account!= null)
                {
                    //update workspace
                    var workspace = new Workspace
                    {
                        AccountId = account.Id,
                        //Name = request.Workspace.Name,
                        //Description = request.Workspace.Description,
                        //Owners = request.Workspace.Owners,
                        Solutions = request.Workspace.Solutions
                    };
                    _workspaceRepository.Update(workspace);

                    var url = Url.RouteUrl("GetByIdRoute", new { id = workspace.Id.ToString() }, Request.Scheme,
                        Request.Host.ToUriComponent());

                    HttpContext.Response.StatusCode = 201;  //201 Created
                    HttpContext.Response.Headers["Location"] = url;
                }
                else
                {
                    HttpContext.Response.StatusCode = 400;  //400 Bad Request
                }
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_workspaceRepository.Remove(new ObjectId(id)))
            {
                return new HttpStatusCodeResult(204);   // 204 No Content
            }
            return HttpNotFound();
        }
    }
}