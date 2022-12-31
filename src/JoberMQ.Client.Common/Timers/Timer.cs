using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JoberMQ.Client.Common.Timers
{
    internal class Timer : ITimer
    {
        private readonly IScheduler scheduler;
        public event Action<TimerModel> Receive;
        Guid _instanceId;

        public Timer(Guid instanceId)
        {
            _instanceId = instanceId;
            var schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.Start().Wait();
        }
        public Guid InstanceId => _instanceId;

        public TimerModel Get(Guid timerId)
        {
            var jobKeys = scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result;
            var jobKey = jobKeys.Where(w => w.Name == timerId.ToString()).FirstOrDefault();
            var jobDData = scheduler.GetJobDetail(jobKey).Result;

            return JsonConvert.DeserializeObject<TimerModel>(jobDData.JobDataMap.Values.FirstOrDefault().ToString());
        }
        public bool Add(TimerModel timer)
        {
            int repeat = 1;
            string cronTime = timer.CronTime;
            bool returnData = false;
            timer.InstanceId = _instanceId;

        repeatWork:
            try
            {
                JobKey jobKey = JobKey.Create(timer.Id.ToString(), timer.TimerGroup);

                IJobDetail job = JobBuilder.Create<TimerAction>()
                    .WithIdentity(jobKey)
                    .UsingJobData("jobdata", JsonConvert.SerializeObject(timer))
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(timer.Id.ToString() + "_Trigger", timer.TimerGroup)
                    .StartNow()
                    .WithCronSchedule(timer.CronTime)
                    .Build();

                var sssss = scheduler.ScheduleJob(job, trigger).Result;

                returnData = true;
            }
            catch (Exception)
            {
                returnData = false;
            }

            if (returnData == false && repeat == 1)
            {
                repeat = 2;
                cronTime = DateToCron(DateTime.Now.AddSeconds(10));
                goto repeatWork;
            }

            return returnData;
        }
        public bool Remove(Guid timerId) => RemoveCommon(timerId);
        public bool Remove(TimerModel timer) => RemoveCommon(timer.Id);
        private bool RemoveCommon(Guid timerId)
        {
            try
            {
                var jobKeys = scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result;
                var jobKey = jobKeys.Where(w => w.Name == timerId.ToString()).FirstOrDefault();
                scheduler.DeleteJob(jobKey);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(TimerModel timer)
        {
            timer.InstanceId = _instanceId;
            var remove = RemoveCommon(timer.Id);
            if (!remove)
                return remove;

            var add = Add(timer);

            return add;
        }

        public void SetReceive(TimerModel timer)
        {
            Receive?.Invoke(timer);
        }

        #region HELPER
        private string DateToCron(DateTime date)
        {
            return $"{date.Second.ToString()} {date.Minute.ToString()} {date.Hour.ToString()} {date.Day.ToString()} {MonthToCronMonth(date.Month)} ? {date.Year.ToString()}";
        }
        private string MonthToCronMonth(int month)
        {
            string response = "";
            switch (month)
            {
                case 1:
                    response = "JAN";
                    break;
                case 2:
                    response = "FEB";
                    break;
                case 3:
                    response = "MAR";
                    break;
                case 4:
                    response = "APR";
                    break;
                case 5:
                    response = "MAY";
                    break;
                case 6:
                    response = "JUN";
                    break;
                case 7:
                    response = "JUL";
                    break;
                case 8:
                    response = "AUG";
                    break;
                case 9:
                    response = "SEP";
                    break;
                case 10:
                    response = "OCT";
                    break;
                case 11:
                    response = "NOV";
                    break;
                case 12:
                    response = "DEC";
                    break;
            }

            return response;
        }
        #endregion
    }

    public class TimerAction : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;


            //var timer = JsonConvert.DeserializeObject<T>(context.JobDetail.JobDataMap.Values.FirstOrDefault().ToString());
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string jobSays = dataMap.GetString("jobdata");
            var timer = JsonConvert.DeserializeObject<TimerModel>(jobSays);


            TimerInstance.Instances.TryGetValue(timer.InstanceId, out var instance);
            Task.Factory.StartNew(() => instance.SetReceive(timer));


            //Task.Factory.StartNew(() => Receive?.Invoke(timer));

            return Task.CompletedTask;
        }

    }
}
