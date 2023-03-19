using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Factories.Account;
using JoberMQ.Client.Net.Factories.Client;
using JoberMQ.Client.Net.Factories.Connect;
using JoberMQ.Client.Net.Factories.Endpoint;
using JoberMQ.Client.Net.Models.DeclareConsume;
using JoberMQ.Client.Net.Models.DeclareDistributor;
using JoberMQ.Client.Net.Models.DeclareQueue;
using JoberMQ.Library.Method.Abstraction;
using JoberMQ.Library.Method.Factories;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Net.Implementation.Client.Default
{
    public class DfClientSocket : IClient
    {
        public DfClientSocket(
            string clientKey,
            string clientGroupKey,
            IConfiguration configuration)
        {
            clientInfo = ClientFactory.CreateClientInfo(configuration);
            clientInfo.ClientKey = clientKey;
            clientInfo.ClientGroupKey = clientGroupKey;

            var endpointDetail = EndpoindFactory.Create(configuration);
            accountInfo = AccountFactory.CreateAccountInfo(configuration, endpointDetail);

            connect = ConnectFactory.Create(configuration, clientInfo, accountInfo, ref ReceiveData, ref ReceiveDataError, ref ReceiveServerActive, configuration.ConnectionRetryTimeout);

            method = MethodFactory.Create(Library.Method.Enums.MethodFactoryEnum.Default);
            declareConsuming = new ConcurrentDictionary<Guid, DeclareConsumeModel>();
        }

        IClientInfo clientInfo;
        public IClientInfo ClientInfo => clientInfo;

        IAccountInfo accountInfo;
        public IAccountInfo AccountInfo => accountInfo;

        IConnect connect;
        public IConnect Connect => connect;

        IMethod method;
        public IMethod Method => method;

        public DeclareConsumeBuilderModel DeclareConsumeBuilder()
            => new DeclareConsumeBuilderModel { DeclareConsumeTransport = new DeclareConsumeTransportModel() };

        public DeclareDistributorBuilderModel DeclareDistributor()
            => new DeclareDistributorBuilderModel { DeclareDistributorTransport = new DeclareDistributorTransportModel() };

        public DeclareQueueBuilderModel DeclareQueue()
            => new DeclareQueueBuilderModel { DeclareQueueTransport = new DeclareQueueTransportModel() };





        public event Action<string> ReceiveData;
        public event Action<string> ReceiveDataError;
        public event Action<bool> ReceiveServerActive;

        
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //isConnect = false;
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }



        ConcurrentDictionary<Guid, DeclareConsumeModel> declareConsuming;
        public ConcurrentDictionary<Guid, DeclareConsumeModel> DeclareConsuming { get => declareConsuming; set => declareConsuming = value; }
    }
}
