using JoberMQ.Client.Net.Enums.Message;
using JoberMQ.Client.Net.Enums.Priority;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Routing;

namespace JoberMQ.Client.Net.Abstraction.Message
{
    public interface IMessage
    {
        MessageTypeEnum MessageType { get; }
        string Message { get; }
        RoutingModel Routing { get; }
        InfoModel Info { get; }
        string GeneralData { get; }
        PriorityTypeEnum PriorityType { get; }
    }
}
