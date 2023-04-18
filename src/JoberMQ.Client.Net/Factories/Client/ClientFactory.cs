using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Implementation.Client.Default;
using JoberMQ.Library.Enums.Client;
using JoberMQ.Library.Enums.Connect;

namespace JoberMQ.Client.Net.Factories.Client
{
    public class ClientFactory
    {
        public static IClient Create(
            string clientKey,
            string clientGroupKey,
            IConfiguration configuration)
        {
            IClient client;

            switch (configuration.ClientFactory)
            {
                case ClientFactoryEnum.Default:
                    switch (configuration.ConnectProtocol)
                    {
                        case ConnectProtocolEnum.Socket:
                            client = new DfClientSocket(clientKey, clientGroupKey, configuration);
                            break;
                        default:
                            client = new DfClientSocket(clientKey, clientGroupKey, configuration);
                            break;
                    }
                    break;
                default:
                    switch (configuration.ConnectProtocol)
                    {
                        case ConnectProtocolEnum.Socket:
                            client = new DfClientSocket(clientKey, clientGroupKey, configuration);
                            break;
                        default:
                            client = new DfClientSocket(clientKey, clientGroupKey, configuration);
                            break;
                    }
                    break;
            }

            return client;
        }
    }
}
