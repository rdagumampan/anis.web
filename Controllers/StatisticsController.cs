using Microsoft.AspNetCore.Mvc;
using Arnis.Web.Models;
using System.Collections.Generic;

namespace Arnis.Web.Controllers
{
    public class StatisticsController : Controller
    {
        [Route("statistics")]
        public IActionResult GetStatistics(string userName, string workspaceName)
        {
            var statistic = new StatisticsVm
            {
                Dependencies = new List<DependencyHitVm>
                {
                    new DependencyHitVm {Name=".NetFramework", Hits= 125 },
                    new DependencyHitVm {Name="Castle.Core", Hits= 30 },
                    new DependencyHitVm {Name="EntityFramework", Hits= 10 },
                    new DependencyHitVm {Name="EntityFramework.SqlServer", Hits= 6 },
                    new DependencyHitVm {Name="Apache.NMS", Hits= 32 },
                    new DependencyHitVm {Name="FluentSharp.CoreLib", Hits= 15 },
                    new DependencyHitVm {Name="Moq", Hits= 41 },
                    new DependencyHitVm {Name="Newtonsoft.Json", Hits= 55 },
                }
            };

            return View(statistic);
        }
    }
}