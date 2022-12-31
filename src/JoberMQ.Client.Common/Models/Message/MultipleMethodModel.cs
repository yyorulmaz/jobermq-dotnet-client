using JoberMQ.Client.Common.Enums.Transport;
using JoberMQ.Client.Common.Models.Option;
using JoberMQ.Client.Common.Models.Routing;
using System.Linq.Expressions;

namespace JoberMQ.Client.Common.Models.Message
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
