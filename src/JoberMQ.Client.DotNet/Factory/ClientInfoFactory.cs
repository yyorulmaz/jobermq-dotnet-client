using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Imp;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.DotNet.Factory
{
    internal class ClientInfoFactory
    {
        public static IClientInfo Create(ClientInfoFactoryEnum clientInfoFactory, ClientTypeEnum clientType, string clientKey, bool isOfflineClient)
        {
            IClientInfo clientInfo;

            switch (clientInfoFactory)
            {
                case ClientInfoFactoryEnum.Default:
                    clientInfo = new ClientInfoDefault(clientType, clientKey, isOfflineClient);
                    break;
                default:
                    clientInfo = new ClientInfoDefault(clientType, clientKey, isOfflineClient);
                    break;
            }

            return clientInfo;
        }
    }
}
