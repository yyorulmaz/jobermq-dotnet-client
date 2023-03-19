using JoberMQ.Client.Net.Enums.Account;
using JoberMQ.Client.Net.Enums.Client;
using JoberMQ.Client.Net.Enums.Connect;
using JoberMQ.Client.Net.Enums.Endpoint;
using JoberMQ.Client.Net.Enums.Protocol;

namespace JoberMQ.Client.Net.Abstraction.Configuration
{
    public interface IConfiguration
    {
        ClientFactoryEnum ClientFactory { get; }
        ClientInfoFactoryEnum ClientInfoFactory { get; }
        ConnectProtocolEnum ConnectProtocol { get; }


        int ConnectionRetryTimeout { get; set; }

        bool IsOfflineMode { get; set; }


        string UserName { get; set; }
        string Password { get; set; }


        EndpoindFactoryEnum EndpoindFactory { get; }
        AccountFactoryEnum AccountFactory { get; }
        AccountInfoFactoryEnum AccountInfoFactory { get; }
        ConnectFactoryEnum ConnectFactory { get; }
        string HostName { get; set; }
        int Port { get; set; }
        int PortSsl { get; set; }
        bool IsSsl { get; set; }
    }
}
