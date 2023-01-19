namespace JoberMQ.Client.Net.Models.Routing
{
    public class RoutingModel
    {
        public string DistributorKey { get; set; }
        public string QueueKey { get; set; }

        public string ClientKey { get; set; }
        public string ClientGroupKey { get; set; }
        public string EventName { get; set; }

        public string StartsWith { get; set; }
        public string Contains { get; set; }
        public string EndsWith { get; set; }
    }
}
