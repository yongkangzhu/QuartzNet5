using System.Collections;
using System;
using System.Collections.Generic;

namespace Quartz.Net5.Scheduler
{
     public class JobSchedules
     {
         public static IEnumerable<JobSchedule> GetJobSchedules()
         {
             return new List<JobSchedule> {
                 new JobSchedule
                 {
                    JobName = "MAC报废送签核",
                    JobDescription = "MAC重新申请需eflow签核",
                    JobGroup = "签核群组",
                    JobUrl = "http://10.24.97.221/job/mac",
                    CronExpression = "0/30 * * * * ?,0/59 * * * * ?",
                    JobData = "hello"                   
                 },
                 new JobSchedule
                 {
                    JobName = "JIT发料任务1",
                    JobDescription = "JIT发料任务1",
                    JobUrl = "http://10.24.97.221/job/mac",
                    JobGroup = "物料群组",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 },
                 new JobSchedule
                 {
                    JobName = "JIT发料任务2",
                    JobDescription = "JIT发料任务2",
                    JobUrl = "http://10.24.97.221/job/mac",
                    JobGroup = "物料群组",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 },
                 new JobSchedule
                 {
                    JobName = "311抛账",
                    JobDescription = "311抛账",
                    JobUrl = "http://10.24.97.221/job/mac",
                    JobGroup = "抛账群组",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 },
                 new JobSchedule
                 {
                    JobName = "261抛账",
                    JobDescription = "261抛账",
                    JobGroup = "抛账群组",
                    JobUrl = "http://10.24.97.221/job/mac",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 },
                 new JobSchedule
                 {
                    JobName = "下载工单",
                    JobDescription = "下载工单",
                    JobGroup = "下载群组",
                    JobUrl = "http://10.24.97.221/job/mac",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 },
                 new JobSchedule
                 {
                    JobName = "下载料号",
                    JobDescription = "下载料号",
                    JobUrl = "http://10.24.97.221/job/mac",
                    JobGroup = "下载群组",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 },
                 new JobSchedule
                 {
                    JobName = "下载BOM",
                    JobDescription = "下载工单",
                    JobGroup = "下载群组",
                    JobUrl = "http://10.24.97.221/job/mac",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 },
                 new JobSchedule
                 {
                    JobName = "下载A",
                    JobDescription = "下载料号",
                    JobGroup = "签核群组",
                    JobUrl = "http://10.24.97.221/job/mac",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 },
                 new JobSchedule
                 {
                    JobName = "下载B",
                    JobDescription = "下载料号",
                    JobGroup = "签核群组",
                    JobUrl = "http://10.24.97.221/job/mac",
                    CronExpression = "0/40 * * * * ?",
                    JobData = "发料"                   
                 }
                 
          
             };

         }

         public static IEnumerable<TriggerModel> GetTriggersInfo()
         {
             return new List<TriggerModel>();
         }

     }

}