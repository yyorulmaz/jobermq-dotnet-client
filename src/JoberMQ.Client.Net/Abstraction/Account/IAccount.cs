using JoberMQ.Client.Net.Abstraction.Endpoint;

namespace JoberMQ.Client.Net.Abstraction.Account
{
    public interface IAccount
    {
        bool IsMaster { get; set; }
        bool IsActive { get; set; }

        string EndpointLogin { get; }
        string EndpointHub { get; }
        string UserName { get; }
        string Password { get; }
        IEndpointDetail EndpointDetail { get; }
    }
}
