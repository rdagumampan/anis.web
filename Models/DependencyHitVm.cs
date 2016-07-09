using System;
using System.Collections.Generic;
using System.Linq;

namespace Arnis.Web.Models
{
    public class DependencyHitVm
    {
        public string Name { get; set; }
        public int Hits { get; set; }
    }

    public class WorkspaceHitVm
    {
        public string Name { get; set; }
        public int Hits { get; set; }
    }

    public class WorkspaceVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Owners { get; set; } = new List<string>();
        public List<LogVm> Logs { get; set; } = new List<LogVm>();
        public List<WorkspaceDependencyVm> Dependencies { get; set; } = new List<WorkspaceDependencyVm>();
    }

    public class LogVm
    {
        public string Message { get; set; }
    }
    public class WorkspaceDependencyVm
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Solution { get; set; }
        public string Project { get; set; }
        public string SolutionFile { get; set; }
        public string ProjectFile { get; set; }

        public string LatestVersion { get; set; }
        public string Status { get; set; }
    }

}
