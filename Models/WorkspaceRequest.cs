using System.Collections.Generic;
using Arnis.Web.ApiModels;

namespace Arnis.Web.Models
{
    public class WorkspaceRequest
    {
        public string ApiKey { get; set; }
        public Workspace Workspace { get; set; } 
    }
}
