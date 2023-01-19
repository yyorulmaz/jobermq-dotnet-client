using JoberMQ.Client.Net.Enums.Status;
using System;

namespace JoberMQ.Client.Net.Models.Status
{
    public class StatusModel
    {
        public bool IsCompleted { get; set; }
        public bool IsError { get; set; }
        public StatusTypeMessageEnum?  StatusTypeMessage { get; set; }
        public DateTime? TempAgainDate { get; set; }
    }
}
