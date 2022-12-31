using JoberMQ.Client.Common.Enums.Declare;

namespace JoberMQ.Client.Common.Models.Declare
{
    internal class DeclareConsumeModel
    {
        public DeclareConsumeTypeEnum DeclareConsumeType { get; set; }
        public string QueueName { get; set; }
        public string Key { get; set; }
    }
}
