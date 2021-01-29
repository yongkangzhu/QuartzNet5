using System.Globalization;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;

namespace Quartz.Net5.Scheduler
{
    public class JobTriggerInfo
    {

        public List<TriggerModel> triggersInfo { get; set; }

        public JobTriggerInfo()
        {
            triggersInfo = new List<TriggerModel>();
        }

        public void addOrUpdate(TriggerModel model)
        {
            //没有 则添加
            var trigger = triggersInfo.FirstOrDefault(x=>x.Name == model.Name);
            if(trigger?.Name == null)
            {
                triggersInfo.Add(model);
            } 
            else 
            {
                //有 则更新job执行的结果
                trigger.Result = model.Result;
            }
 

        }
    }


}