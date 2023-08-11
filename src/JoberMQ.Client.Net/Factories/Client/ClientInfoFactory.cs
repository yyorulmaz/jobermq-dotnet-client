using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Implementation.Client.Default;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Net.Factories.Client
{
    internal class ClientInfoFactory
    {
        public static IClientInfo Create(ClientInfoFactoryEnum clientInfoFactory, ClientTypeEnum clientType, string clientKey, bool isOfflineClient)
        {
            IClientInfo clientInfo;

            switch (clientInfoFactory)
            {
                case ClientInfoFactoryEnum.Default:
                    clientInfo = new DefaultClientInfo(clientType, clientKey, isOfflineClient);
                    break;
                default:
                    clientInfo = new DefaultClientInfo(clientType, clientKey, isOfflineClient);
                    break;
            }

            return clientInfo;
        }
    }
}
