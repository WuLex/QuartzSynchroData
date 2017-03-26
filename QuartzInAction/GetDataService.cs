using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Quartz;
using Quartz.Impl;
using Common.Logging;
using QuartzInAction.Common;
using System.Configuration;

namespace QuartzInAction
{
    public partial class GetDataService : ServiceBase
    {
        private readonly ILog logger;
        private IScheduler scheduler;
        //时间间隔
        private readonly string StrCron = ConfigurationManager.AppSettings["cron"] ?? "* 10 * * * ?";


        public GetDataService()
        {
            InitializeComponent();

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        protected override void OnStart(string[] args)
        {
            if (!scheduler.IsStarted)
            {
                //启动调度器
                scheduler.Start();
                //新建一个任务
                IJobDetail job = JobBuilder.Create<GetOrdersJob>().WithIdentity("GetOrdersJob", "GetOrdersJobGroup").Build();
                //新建一个触发器
                ITrigger trigger = TriggerBuilder.Create().StartNow().WithCronSchedule(StrCron).Build();
                //将任务与触发器关联起来放到调度器中
                scheduler.ScheduleJob(job, trigger);
                logger.Info("Quarzt 数据同步服务开启");
            }
            Loger.WriteFile(DateTime.Now + ":启动了服务!");

        }

        protected override void OnStop()
        {
            scheduler.Shutdown(false);
            Loger.WriteFile(DateTime.Now + ":停止执行服务!");
        }

        protected override void OnPause()
        {
            scheduler.PauseAll();
        }

        protected override void OnContinue()
        {
            scheduler.ResumeAll();
        }
    }
}
