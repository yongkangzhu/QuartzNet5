using System.Globalization;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quartz.Net5.Scheduler;

namespace Quartz.Net5.Scheduler.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class WorkflowController : ControllerBase
    {

        [HttpPost]
        public string Job(test t)
        {
            return $"{t.JobName} : {t.group}";
        }
    }

    public class test
    {
        public string JobName { get; set; }
        public string group { get; set; }
    }
}