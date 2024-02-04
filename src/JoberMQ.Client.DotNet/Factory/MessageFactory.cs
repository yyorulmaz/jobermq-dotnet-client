using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Imp;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Priority;
using JoberMQ.Common.Models.Info;
using JoberMQ.Common.Models.Routing;

namespace JoberMQ.Client.DotNet.Factory
{
    internal class MessageFactory
    {
        public static IMessage Create(MessageTypeEnum messageType, string message, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType, bool isConsumingRetryPause, int consumingRetryMaxCount)
            => new MessageDefault(messageType, message, routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
    }
}
