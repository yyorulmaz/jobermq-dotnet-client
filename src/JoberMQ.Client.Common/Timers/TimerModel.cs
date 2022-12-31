using System;

namespace JoberMQ.Client.Common.Timers
{
    internal class TimerModel
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public string CronTime { get; set; }
        public string TimerGroup { get; set; }
        public string TimerType { get; set; }
        public string ExecuteMethodCall { get; set; }
        public string Data { get; set; }
    }
}
