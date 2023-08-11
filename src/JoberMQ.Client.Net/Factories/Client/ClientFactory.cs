using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Implementation.Client.Default;
using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Enums.Connect;

namespace JoberMQ.Client.Net.Factories.Client
{
    public class ClientFactory
    {
        public static IClient Create(string clientKey, IConfiguration configuration)
        {
            IClient client;

            switch (configuration.ClientFactory)
            {
                case ClientFactoryEnum.Default:
                    switch (configuration.ConnectProtocol)
                    {
                        case ConnectProtocolEnum.Socket:
                            client = new DefaultClientSocket(clientKey, configuration);
                            break;
                        default:
                            client = new DefaultClientSocket(clientKey, configuration);
                            break;
                    }
                    break;
                default:
                    switch (configuration.ConnectProtocol)
                    {
                        case ConnectProtocolEnum.Socket:
                            client = new DefaultClientSocket(clientKey, configuration);
                            break;
                        default:
                            client = new DefaultClientSocket(clientKey, configuration);
                            break;
                    }
                    break;
            }

            return client;
        }
    }
}
