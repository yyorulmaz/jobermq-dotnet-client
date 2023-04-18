using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Factories.Endpoint;
using JoberMQ.Library.Enums.Account;
using JoberMQ.Library.Enums.Client;
using JoberMQ.Library.Enums.Connect;
using JoberMQ.Library.Enums.Endpoint;

namespace JoberMQ.Client.Net.Constants
{
    internal class ClientConst
    {
        internal const ConnectFactoryEnum ConnectFactory = ConnectFactoryEnum.Default;
        internal const AccountFactoryEnum AccountFactory = AccountFactoryEnum.Default;
        internal const ConnectProtocolEnum ConnectProtocol = ConnectProtocolEnum.Socket;
        internal const ClientFactoryEnum ClientFactory = ClientFactoryEnum.Default;
        internal const ClientInfoFactoryEnum ClientInfoFactory = ClientInfoFactoryEnum.Default;


        internal const ClientTypeEnum ClientType = ClientTypeEnum.Normal;
        internal const bool IsOfflineClient = true;


        internal const string UserName = "jobermq";
        internal const string Password = "jobermq";


        internal const int ConnectionRetryTimeoutMin = 2000;
        internal const int ConnectionRetryTimeout = 5000;
        internal const bool AutoReconnect = true;


        internal const string DistributorDefaultDirectSpecialKey = "distributor.default.direct.special";
        internal const string DistributorDefaultDirectGroupKey = "distributor.default.direct.group";
        internal const string DistributorDefaultDirectQueueKey = "distributor.default.direct.queue";
        internal const string DistributorDefaultEventKey = "distributor.default.event";
        internal const string QueueDefaultSpecialKey = "queue.default.special";
      



        internal const EndpointFactoryEnum EndpointFactoryy = EndpointFactoryEnum.Default;
        internal static IEndpoint EndpointLogin = SetEndpointLogin();
        internal static IEndpoint EndpointHub = SetEndpointHub();

        private static IEndpoint SetEndpointLogin()
            => EndpointFactory.Create(EndpointFactoryEnum.Default, true, "localhost", 7654, 7655, "account/login");
        private static IEndpoint SetEndpointHub()
            => EndpointFactory.Create(EndpointFactoryEnum.Default, true, "localhost", 7654, 7655, "JoberHub");


        internal const bool IsConsumingRetryPause = false;
        internal const int ConsumingRetryMaxCount = 10;



        internal const bool TextMessageReceiveAutoCompleted = true;
        
    }
}
