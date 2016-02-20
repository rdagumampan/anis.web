using System.Collections.Generic;
using Arnis.Web.Models;
using Arnis.Web.Repositiories;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;

namespace Arnis.Web.Controllers
{
    [Route("api/[controller]")]
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkspaceController(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        [HttpGet]
        public IEnumerable<Workspace> GetAll()
        {
            var speakers = _workspaceRepository.All();
            return speakers;
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
        public void Create([FromBody] Workspace speaker)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;
            }
            else
            {
                _workspaceRepository.Add(speaker);

                var url = Url.RouteUrl("GetByIdRoute", new { id = speaker.Id.ToString() }, Request.Scheme,
                    Request.Host.ToUriComponent());
                HttpContext.Response.StatusCode = 201;
                HttpContext.Response.Headers["Location"] = url;
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_workspaceRepository.Remove(new ObjectId(id)))
            {
                return new HttpStatusCodeResult(204); // 204 No Content
            }
            return HttpNotFound();
        }
    }
}