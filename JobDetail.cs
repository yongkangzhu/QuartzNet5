using System.Threading;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using Common;
using System.Collections.Generic;
using System.Linq;

namespace Quartz.Net5.Scheduler
{
    //[DisallowConcurrentExecution]
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;
        private readonly JobTriggerInfo _jobTriggerInfo ;
        public HelloWorldJob(ILogger<HelloWorldJob> logger,JobTriggerInfo JobTriggerInfo)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jobTriggerInfo = JobTriggerInfo ?? throw new ArgumentNullException(nameof(JobTriggerInfo));
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var d = context.JobDetail.JobDataMap.Get("data") as JobSchedule;
                
                string triggerName =  context.Trigger.Key.Name;
                string jobName = context.JobDetail.Key.Name;


                var next_time = context.NextFireTimeUtc.Value.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");             
                //string body = "{\"jobName\": \"string\", 	\"group\": \"string\" }";
                string result = "OK";
                //result = HttpHelper.HttpPost("http://localhost:5000/Workflow/job",body:body,contentType:"application/json");
                _jobTriggerInfo.addOrUpdate(new TriggerModel{
                    Name = triggerName,
                    Result = result
                });

                Thread.Sleep(5000);
                _logger.LogInformation($"Job:{d.JobName} run with data:{d.JobData},next exec time {next_time},result:{result}");
                
                
            }
            catch (System.Exception ex)
            {             
                _logger.LogError(ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}