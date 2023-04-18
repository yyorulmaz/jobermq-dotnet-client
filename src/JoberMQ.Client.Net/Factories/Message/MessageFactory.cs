using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Client.Net.Implementation.Message.Default;
using JoberMQ.Library.Enums.Message;
using JoberMQ.Library.Enums.Priority;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Factories.Message
{
    internal class MessageFactory
    {
        public static IMessage Create(MessageTypeEnum messageType, string message, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType, bool isConsumingRetryPause, int consumingRetryMaxCount)
            => new DfMessage(messageType, message, routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
    }
}
