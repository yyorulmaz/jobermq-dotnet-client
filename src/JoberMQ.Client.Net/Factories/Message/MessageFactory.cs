using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Enums.Message;
using JoberMQ.Client.Net.Enums.Priority;
using JoberMQ.Client.Net.Implementation.Message.Default;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Routing;

namespace JoberMQ.Client.Net.Factories.Message
{
    internal class MessageFactory
    {
        public static IMessage Create(MessageTypeEnum messageType,string message, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType)
            => new DfMessage(messageType, message, routing, info, generalData, priorityType);
    }
}
