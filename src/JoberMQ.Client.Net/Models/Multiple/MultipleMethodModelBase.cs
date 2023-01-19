using JoberMQ.Client.Net.Extensions.Job;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Routing;

namespace JoberMQ.Client.Net.Models.Multiple
{
    public class MultipleMethodModelBase
    {
        public RoutingModel Routing { get; set; }
        public InfoModel Info { get; set; }
    }
}
