using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Priority;
using JoberMQ.Common.Models.Consuming;
using JoberMQ.Common.Models.Info;
using JoberMQ.Common.Models.Routing;

namespace JoberMQ.Client.DotNet.Abs
{
    public interface IMessage
    {
        MessageTypeEnum MessageType { get; }
        string Message { get; }
        RoutingModel Routing { get; }
        InfoModel Info { get; }
        string GeneralData { get; }
        PriorityTypeEnum PriorityType { get; }
        ConsumingModel MessageConsuming { get; set; }
    }
}
