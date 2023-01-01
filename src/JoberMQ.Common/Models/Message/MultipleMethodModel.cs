using JoberMQ.Common.Enums.Transport;
using JoberMQ.Common.Models.Option;
using JoberMQ.Common.Models.Routing;
using System;
using System.Linq.Expressions;

namespace JoberMQ.Common.Models.Message
{
    public class MultipleMethodModel
    {
        public Expression<Action> MethodCall { get; set; }
        public TransportTypeEnum JobTransportType { get; set; }
        public RoutingModel Routing { get; set; }
        public string EventName { get; set; }
        public OptionModel Option { get; set; }
    }
}
