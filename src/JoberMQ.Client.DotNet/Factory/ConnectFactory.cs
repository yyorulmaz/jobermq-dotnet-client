using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Imp;
using JoberMQ.Common.Enums.Connect;

namespace JoberMQ.Client.DotNet.Factory
{
    internal class ConnectFactory
    {
        public static IConnect Create(ConnectFactoryEnum connectFactory, int retryTimeout, bool autoReconnect, IAccount account, IClientInfo clientInfo)
        {
            IConnect connect;

            switch (connectFactory)
            {
                case ConnectFactoryEnum.Default:
                    connect = new ConnectSocket(retryTimeout, autoReconnect, account, clientInfo);
                    break;
                default:
                    connect = new ConnectSocket(retryTimeout, autoReconnect, account, clientInfo);
                    break;
            }

            return connect;
        }
    }
}
