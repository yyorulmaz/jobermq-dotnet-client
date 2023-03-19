using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Models.DeclareConsume;
using JoberMQ.Client.Net.Models.DeclareDistributor;
using JoberMQ.Client.Net.Models.DeclareQueue;
using JoberMQ.Library.Method.Abstraction;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClient
    {
        IClientInfo ClientInfo { get; }
        IAccountInfo AccountInfo { get; }
        IConnect Connect { get; }
        IMethod Method { get; }


        event Action<string> ReceiveData;
        event Action<string> ReceiveDataError;
        event Action<bool> ReceiveServerActive;

        DeclareConsumeBuilderModel DeclareConsumeBuilder();
        DeclareDistributorBuilderModel DeclareDistributor();
        DeclareQueueBuilderModel DeclareQueue();

        ConcurrentDictionary<Guid, DeclareConsumeModel> DeclareConsuming { get; set; }
    }
}
