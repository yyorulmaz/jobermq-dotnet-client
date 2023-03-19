using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Enums.Connect;
using JoberMQ.Client.Net.Implementation.Connect.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Factories.Connect
{
    public class ConnectFactory
    {
        public static IConnect Create(
            IConfiguration configuration,
            IClientInfo clientInfo,
            IAccountInfo accountInfo,
            ref Action<string> receiveData,
            ref Action<string> receiveDataError,
            ref Action<bool> receiveServerActive,
            int connectionRetryTimeout)
        {
            IConnect connect;

            switch (configuration.ConnectFactory)
            {
                case ConnectFactoryEnum.Default:
                    connect = new DfConnect(clientInfo, accountInfo, ref receiveData, ref receiveDataError, ref receiveServerActive, connectionRetryTimeout);
                    break;
                default:
                    connect = new DfConnect(clientInfo, accountInfo, ref receiveData, ref receiveDataError, ref receiveServerActive, connectionRetryTimeout);
                    break;
            }

            return connect;
        }
    }
}
