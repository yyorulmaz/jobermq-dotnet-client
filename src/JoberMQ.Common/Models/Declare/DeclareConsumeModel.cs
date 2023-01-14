using JoberMQ.Common.Enums.Declare;

namespace JoberMQ.Common.Models.Declare
{
    public class DeclareConsumeModel
    {
        public DeclareConsumeTypeEnum DeclareConsumeType { get; set; }
        public string QueueKey { get; set; }
        public string Key { get; set; }
    }
}
