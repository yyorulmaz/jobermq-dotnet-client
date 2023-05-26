using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Implementation.Message.Default;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Priority;
using JoberMQ.Common.Models.Info;
using JoberMQ.Common.Models.Routing;

namespace JoberMQ.Client.Net.Factories.Message
{
    internal class MessageFactory
    {
        public static IMessage Create(MessageTypeEnum messageType, string message, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType, bool isConsumingRetryPause, int consumingRetryMaxCount)
            => new DfMessage(messageType, message, routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
    }
}
