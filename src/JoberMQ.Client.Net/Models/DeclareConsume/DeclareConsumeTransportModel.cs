using JoberMQ.Client.Net.Enums.Declare;

namespace JoberMQ.Client.Net.Models.DeclareConsume
{
    public class DeclareConsumeTransportModel
    {
        public DeclareConsumeOperationTypeEnum DeclareConsumeOperationType { get; internal set; }
        public string DeclareKey { get; internal set; }
    }
}
