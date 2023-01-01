namespace JoberMQ.Common.Models.Cluster
{
    internal class ServerEndpointModel
    {
        public bool IsActive { get; set; }

        public bool IsSsl { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string ActionHub { get; set; }
        public string ActionLogin { get; set; }

        public int Counter { get; set; } = 0;
    }
}
