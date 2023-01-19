using JoberMQ.Client.Net.Enums.Message;
using JoberMQ.Client.Net.Enums.Priority;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Routing;

namespace JoberMQ.Client.Net.Models.Message
{
    public class MessageModel
    {
        public MessageTypeEnum MessageType { get; set; }
        public string Message { get; set; }
        public RoutingModel Routing { get; set; }
        public InfoModel Info { get; set; }
        public string GeneralData { get; set; }
        public PriorityTypeEnum PriorityType { get; set; } = PriorityTypeEnum.None;
    }
}
