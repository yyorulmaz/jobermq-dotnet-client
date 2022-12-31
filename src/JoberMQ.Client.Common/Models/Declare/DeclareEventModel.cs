using JoberMQ.Client.Common.Enums.Declare;

namespace JoberMQ.Client.Common.Models.Declare
{
    internal class DeclareEventModel
    {
        public DeclareEventTypeEnum DeclareEventType { get; set; }
        public string EventName { get; set; }
    }
}
