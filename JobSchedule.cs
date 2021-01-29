using System.Globalization;
using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace Quartz.Net5.Scheduler
{
    /// <summary>
    /// Job调度中间对象
    /// </summary>
    public class JobSchedule
    {
        /// <summary>
        /// Job名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// Job描述
        /// </summary>
        public string JobDescription { get; set; }

        /// <summary>
        /// Job群组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; }
        /// <summary>
        /// Job的接口地址
        /// </summary>
        public string JobUrl { get; set; }
        /// <summary>
        /// Job的执行参数
        /// </summary>
        public string JobData { get; set; }


    }


    public class JobModel
    {

        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string JobUrl { get; set; }
        public string JobGroup { get; set; }

        public List<TriggerModel> Triggers { get; set; } = new List<TriggerModel>();
       
    }

    public class TriggerModel
    {

        public string Name { get; set; }
        public string Cron { get; set; }
        public string State { get; set; }
        public string Result { get; set; }
        public string NextExecTime { get; set; }
       

    }
}