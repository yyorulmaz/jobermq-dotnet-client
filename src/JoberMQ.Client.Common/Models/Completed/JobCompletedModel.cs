using JoberMQ.Client.Common.Enums.Routing;

namespace JoberMQ.Client.Common.Models.Completed
{
    internal class JobCompletedModel
    {
        public Guid JobMessageId { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public byte[] ReturnData { get; set; }
        public RoutingTypeEnum RoutingType { get; set; }
    }
}
