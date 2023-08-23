using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Imp;
using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Enums.Connect;

namespace JoberMQ.Client.DotNet.Factory
{
    internal class ClientFactory
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
                            client = new ClientSocket(clientKey, configuration);
                            break;
                        default:
                            client = new ClientSocket(clientKey, configuration);
                            break;
                    }
                    break;
                default:
                    switch (configuration.ConnectProtocol)
                    {
                        case ConnectProtocolEnum.Socket:
                            client = new ClientSocket(clientKey, configuration);
                            break;
                        default:
                            client = new ClientSocket(clientKey, configuration);
                            break;
                    }
                    break;
            }

            return client;
        }
    }
}
