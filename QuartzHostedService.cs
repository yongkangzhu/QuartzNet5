using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Quartz.Impl.Matchers;

namespace Quartz.Net5.Scheduler
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
            _jobFactory = jobFactory ?? throw new ArgumentNullException(nameof(jobFactory));
            _jobSchedules = jobSchedules ?? throw new ArgumentNullException(nameof(jobSchedules));
        }
        public static IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);
                await Scheduler.ScheduleJob(job, trigger,true, cancellationToken);
                
            }
            await Scheduler.Start(cancellationToken);

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);

        }

        private  IJobDetail CreateJob(JobSchedule schedule)
        {
            var dic = new Dictionary<string,JobSchedule>();
            dic.Add("data",schedule);

            return JobBuilder
                .Create(typeof(HelloWorldJob))
                .WithIdentity(schedule.JobName,schedule.JobGroup)
                .WithDescription(schedule.JobDescription)
                .UsingJobData(new JobDataMap(dic))           
                .Build();
        }

        private  IReadOnlyCollection<ITrigger> CreateTrigger(JobSchedule schedule)
        {
            List<ITrigger> triggers = new List<ITrigger>();

            var crons = schedule.CronExpression.Split(',');

            for (int i = 0; i < crons.Length; i++)
            {
                var Builder = TriggerBuilder
                            .Create()
                            .WithIdentity($"{schedule.JobName}.trigger{i+1}")
                            .WithCronSchedule(crons[i])
                            .WithDescription(crons[i])
                            .Build();
                triggers.Add(Builder);       
            }

            return triggers;              
        }

        public Task PauseTrigger(string triggerKey)
        {
                Scheduler.PauseTrigger(new TriggerKey(triggerKey)); 
            return Task.CompletedTask;           
        }

        public Task ResumeTrigger(string triggerKey)
        {
            Scheduler.ResumeTrigger(new TriggerKey(triggerKey)); 
            return Task.CompletedTask;           
        }

        public Task PauseJob(string JobName,string group)
        {
            Scheduler.PauseJob(new JobKey(JobName,group)); 
            return Task.CompletedTask;           
        }

        public Task PauseJobs(string group)
        {
            Scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals(group)); 
            return Task.CompletedTask;           
        }
    
        public Task ResumeJob(string JobName,string group)
        {
            Scheduler.ResumeJob(new JobKey(JobName,group)); 
            return Task.CompletedTask;           
        }

        public Task ResumeJobs(string group)
        {
            Scheduler.ResumeJobs(GroupMatcher<JobKey>.GroupEquals(group)); 
            return Task.CompletedTask;           
        }

        public Task PauseAll()
        {
            Scheduler.PauseAll();   
            return Task.CompletedTask;           
        }

        public Task ResumeAll()
        {
            Scheduler.ResumeAll(); 
            
            return Task.CompletedTask;           
        }
    }
}