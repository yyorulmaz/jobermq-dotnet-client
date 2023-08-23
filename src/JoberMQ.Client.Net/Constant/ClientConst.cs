﻿using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Factories.Endpoint;
using JoberMQ.Common.Enums.Account;
using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Enums.Connect;
using JoberMQ.Common.Enums.Endpoint;

namespace JoberMQ.Client.Net.Constant
{
    public class ClientConst
    {
        internal const ClientFactoryEnum ClientFactory = ClientFactoryEnum.Default;
        internal const ClientInfoFactoryEnum ClientInfoFactory = ClientInfoFactoryEnum.Default;
        internal const ClientTypeEnum ClientType = ClientTypeEnum.Normal;
        internal const bool IsOfflineClient = true;


        internal const ConnectFactoryEnum ConnectFactory = ConnectFactoryEnum.Default;
        internal const ConnectProtocolEnum ConnectProtocol = ConnectProtocolEnum.Socket;
        internal const int ConnectionRetryTimeoutMin = 2000;
        internal const int ConnectionRetryTimeout = 5000;
        internal const bool AutoReconnect = true;
        internal static IEndpoint EndpointLogin = SetEndpointLogin();
        internal static IEndpoint EndpointHub = SetEndpointHub();
        private static IEndpoint SetEndpointLogin()
            => EndpointFactory.Create(EndpointFactoryEnum.Default, true, "localhost", 7654, 7655, "account/login");
        private static IEndpoint SetEndpointHub()
            => EndpointFactory.Create(EndpointFactoryEnum.Default, true, "localhost", 7654, 7655, "JoberHub");




        internal const AccountFactoryEnum AccountFactory = AccountFactoryEnum.Default;
        internal const string UserName = "jobermq";
        internal const string Password = "jobermq";



        internal const bool TextMessageReceiveAutoCompleted = true;
        internal const bool IsConsumingRetryPause = false;
        internal const int ConsumingRetryMaxCount = 10;
        internal const bool IsDbTextSave = true;



        internal const string DefaultDistributorDirectKey = "def.dis.direct";
        internal const string DefaultQueueClientKey = "def.que.clientkey.free";


        public const string Default_1m_Queue = "def.que.all.free.1m";
        public const string Default_5m_Queue = "def.que.all.free.5m";
        public const string Default_15m_Queue = "def.que.all.free.15m";
        public const string Default_1h_Queue = "def.que.all.free.1h";
        public const string Default_4h_Queue = "def.que.all.free.4h";
        public const string Default_1d_Queue = "def.que.all.free.1d";

    }
}
