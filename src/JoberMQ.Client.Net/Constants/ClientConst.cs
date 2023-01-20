using JoberMQ.Client.Net.Enums.Account;
using JoberMQ.Client.Net.Enums.Client;
using JoberMQ.Client.Net.Enums.Endpoint;
using JoberMQ.Client.Net.Enums.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Constants
{
    internal class ClientConst
    {
        internal const ClientFactoryEnum ClientFactory = ClientFactoryEnum.Default;
        internal const ConnectProtocolEnum ConnectProtocol = ConnectProtocolEnum.Socket;
        internal const bool IsOfflineMode = true;
        internal const int ConnectionRetryTimeoutMin = 2000;
        internal const int ConnectionRetryTimeout = 5000;

        internal const string UserName = "jobermq";
        internal const string Password = "jobermq";


        
        internal const EndpoindFactoryEnum EndpoindFactory = EndpoindFactoryEnum.Default;
        internal const AccountFactoryEnum AccountFactory = AccountFactoryEnum.Default;
        internal const string HostName = "localhost";
        internal const int Port = 7654;
        internal const int PortSsl = 7655;
        internal const bool IsSsl = true;

        internal const string ActionHub = "JoberHub";
        internal const string ActionLogin = "account/login";



        internal const string DefaultDistributorDirectKey = "dis.default.direct";
        internal const string DefaultQueueSpecialKey = "queue.default.special";

    }
}
