using JoberMQ.Client.Common.Enums.Transport;
using JoberMQ.Client.Common.Models.Option;
using JoberMQ.Client.Common.Models.Routing;

namespace JoberMQ.Client.Common.Models.Message
{
    public class MultipleMessageModel
    {
        public string Message { get; set; }
        public TransportTypeEnum JobTransportType { get; set; }
        public RoutingModel Routing { get; set; }
        public string EventName { get; set; }
        public OptionModel Option { get; set; }
    }
}
