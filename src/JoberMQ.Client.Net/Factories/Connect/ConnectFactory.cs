using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Implementation.Connect.Default;
using JoberMQ.Common.Enums.Connect;

namespace JoberMQ.Client.Net.Factories.Connect
{
    internal class ConnectFactory
    {
        public static IConnect Create(ConnectFactoryEnum connectFactory, int retryTimeout, bool autoReconnect, IAccount account, IClientInfo clientInfo)
        {
            IConnect connect;

            switch (connectFactory)
            {
                case ConnectFactoryEnum.Default:
                    connect = new DefaultConnect(retryTimeout, autoReconnect, account, clientInfo);
                    break;
                default:
                    connect = new DefaultConnect(retryTimeout, autoReconnect, account, clientInfo);
                    break;
            }

            return connect;
        }
    }
}
