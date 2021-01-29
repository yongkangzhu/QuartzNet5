using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quartz.Net5.Scheduler;
using Quartz.Impl.Matchers;

namespace Quartz.Net5.Scheduler.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ScheduleManagerController : ControllerBase
    {
        private readonly ILogger<ScheduleManagerController> _logger;
        private readonly IEnumerable<JobSchedule> _jobSchedules ;
        private readonly QuartzHostedService _quartzHostedService ;
        private readonly JobTriggerInfo _jobTriggerInfo ;
        public ScheduleManagerController(ILogger<ScheduleManagerController> logger,
                                        IEnumerable<JobSchedule> jobSchedules,
                                        QuartzHostedService quartzHostedService,
                                        JobTriggerInfo JobTriggerInfo)
        {
            _logger = logger;
            _jobSchedules = jobSchedules;
            _quartzHostedService = quartzHostedService;
            _jobTriggerInfo = JobTriggerInfo ?? throw new ArgumentNullException(nameof(JobTriggerInfo));
        }

        [HttpGet]
        public async Task<List<List<JobModel>>>Get()
        {
            List<JobModel> jobs = new List<JobModel>();
            var groups = await QuartzHostedService.Scheduler.GetJobGroupNames();
            foreach (var groupName in groups)
            {
                foreach (var jobKey in  await QuartzHostedService.Scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName)))
                {
                    JobModel jobModel = new JobModel
                    {
                        JobName = jobKey.Name,
                        JobGroup = groupName
                    };
                    jobModel.JobUrl = _jobSchedules.First(x=>x.JobName == jobKey.Name)?.JobUrl;
                    // string jobName = jobKey.Name;
                    // string jobGroup = jobKey.Group;
                    List<TriggerModel> triggerModels = new List<TriggerModel>();

                    var triggers = await QuartzHostedService.Scheduler.GetTriggersOfJob(jobKey);
                    foreach (ITrigger trigger in triggers)
                    {
                        var state = await QuartzHostedService.Scheduler.GetTriggerState(trigger.Key);
                        TriggerModel triggerModel = new TriggerModel
                        {
                            Name = trigger.Key.Name,
                            Cron = trigger.Description,
                            State = Enum.GetName(state.GetType(), state),
                            NextExecTime = trigger.GetNextFireTimeUtc().Value.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss"),
                            Result = _jobTriggerInfo.triggersInfo.FirstOrDefault(x=>x.Name ==trigger.Key.Name)?.Result
                            
                        };
                        triggerModels.Add(triggerModel);
                        // string name = trigger.Key.Name;
                        // string cron = trigger.Description;
                        // string next_time = trigger.GetNextFireTimeUtc().Value.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");
                             
                    }    

                    jobModel.Triggers = triggerModels;   
                    jobs.Add(jobModel);
                }
            }

            var d = jobs.GroupBy(x=>x.JobGroup).Select(x=>x.ToList()).ToList();
            
            return  await Task.FromResult(d);
            //return _jobSchedules;
        }

        [HttpPost]
        public string PauseOrResumeTrigger(string TriggerName,string state)
        {
            if(state == "Normal")  
            {
                _quartzHostedService.PauseTrigger(TriggerName);
                return $"{TriggerName} Paused";
            }  
            else
            {
                _quartzHostedService.ResumeTrigger(TriggerName);
                return $"{TriggerName} Resumed";
            }
                      
        }
       
        [HttpPost]
        public string PauseOrResumeJob(string JobName,string group,string action)
        {
            if(action == "Pause")
            {
                _quartzHostedService.PauseJob(JobName,group);
                return $"{JobName} Paused";
            }
            else
            {
                 _quartzHostedService.ResumeJob(JobName,group);
                return $"{JobName} Resumed";
            }
           
        }

        [HttpPost]
        public string PauseOrResumeGroup(string group,string action)
        {
            if(action == "Pause")
            {
                _quartzHostedService.PauseJobs(group);
                return $"{group} Paused";
            }
            else
            {
                _quartzHostedService.ResumeJobs(group);
                return $"{group} Resumed";
            }
            
        }

        [HttpPost]
        public string PauseOrResumeAll(string action)
        {
            if(action == "Pause")
            {
                _quartzHostedService.PauseAll();
                return "Pause All Jobs";
            }
            else
            {
                _quartzHostedService.ResumeAll();
            return $"Resume All Jobs";
            }
            
        }

    }
}
