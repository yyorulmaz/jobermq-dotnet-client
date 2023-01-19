namespace JoberMQ.Client.Net.Abstraction.Endpoint
{
    public interface IEndpointDetail
    {
        bool IsSsl { get; set; }
        string HostName { get; set; }
        int Port { get; set; }
        int PortSsl { get; set; }
        string ActionHub { get; set; }
        string ActionLogin { get; set; }

        string GetEndpointLogin();
        string GetEndpointHub();
    }
}
