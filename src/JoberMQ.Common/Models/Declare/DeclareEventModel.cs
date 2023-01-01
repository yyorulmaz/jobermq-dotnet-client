using JoberMQ.Common.Enums.Declare;

namespace JoberMQ.Common.Models.Declare
{
    internal class DeclareEventModel
    {
        public DeclareEventTypeEnum DeclareEventType { get; set; }
        public string EventName { get; set; }
    }
}
