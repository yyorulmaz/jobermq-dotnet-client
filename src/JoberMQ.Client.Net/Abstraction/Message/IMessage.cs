using JoberMQ.Library.Enums.Message;
using JoberMQ.Library.Enums.Priority;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Routing;
using System;
using System.Collections.Generic;
using System.Text;

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
        ConsumingModel MessageConsuming { get; set; }
    }
}
