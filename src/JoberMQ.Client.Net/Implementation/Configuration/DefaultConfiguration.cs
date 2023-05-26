using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Constant;
using JoberMQ.Client.Net.Factories.Endpoint;
using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Enums.Connect;
using JoberMQ.Common.Enums.Endpoint;

namespace JoberMQ.Client.Net.Implementation.Configuration
{
    public class DefaultConfiguration : IConfiguration
    {
        ClientFactoryEnum clientFactory = ClientConst.ClientFactory;
        public ClientFactoryEnum ClientFactory { get => clientFactory; set => clientFactory = value; }
        ConnectProtocolEnum connectProtocol = ClientConst.ConnectProtocol;
        public ConnectProtocolEnum ConnectProtocol { get => connectProtocol; set => connectProtocol = value; }
        ClientTypeEnum IConfiguration.ClientType => ClientConst.ClientType;
        bool IConfiguration.IsOfflineClient => ClientConst.IsOfflineClient;


        int IConfiguration.ConnectionRetryTimeoutMin => ClientConst.ConnectionRetryTimeoutMin;
        int connectionRetryTimeout = ClientConst.ConnectionRetryTimeout;
        public int ConnectionRetryTimeout { get => connectionRetryTimeout; set => connectionRetryTimeout = value; }
        bool IConfiguration.AutoReconnect => ClientConst.AutoReconnect;


        IEndpoint endpointLogin = ClientConst.EndpointLogin;
        IEndpoint IConfiguration.EndpointLogin { get => endpointLogin; set => endpointLogin = value; }
        IEndpoint endpointHub = ClientConst.EndpointHub;
        IEndpoint IConfiguration.EndpointHub { get => endpointHub; set => endpointHub = value; }


        bool textMessageReceiveAutoCompleted = ClientConst.TextMessageReceiveAutoCompleted;
        public bool TextMessageReceiveAutoCompleted { get => textMessageReceiveAutoCompleted; set => textMessageReceiveAutoCompleted = value; }

        public void SetEndpointLogin(bool IsSsl, string HostName, int Port, int PortSsl, string Action)
        {
            endpointLogin = EndpointFactory.Create(EndpointFactoryEnum.Default, IsSsl, HostName, Port, PortSsl, Action);
        }
        public void SetEndpointHub(bool IsSsl, string HostName, int Port, int PortSsl, string Action)
        {
            endpointHub = EndpointFactory.Create(EndpointFactoryEnum.Default, IsSsl, HostName, Port, PortSsl, Action);
        }
    }
}
