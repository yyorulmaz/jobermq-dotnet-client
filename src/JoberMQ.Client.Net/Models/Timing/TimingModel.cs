using JoberMQ.Client.Net.Enums.Timing;
using System;

namespace JoberMQ.Client.Net.Models.Timing
{
    public class TimingModel
    {
        public TimingTypeEnum TimingType { get; set; }
        public ScheduleTypeEnum ScheduleType { get; set; }
        public string CronTime { get; set; }


        public int? DelayedSecond { get; set; }
        public int? ExecuteCountMax { get; set; }
        public int CreatedCount { get; set; }
        public bool IsCountMax { get; set; }


        public bool IsTrigger { get; set; }
        public bool ErrorWorkflowStop { get; set; }
        public Guid? TriggerJobId { get; set; }
        public bool IsTriggerMain { get; set; }


        public Guid? TriggerGroupsId { get; set; }
    }
}
