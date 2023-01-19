using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Models.Producer;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Method.Abstraction;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClient : IDisposable
    {
        ProducerModel Producer { get; }
        bool IsClientActive { get; }
        bool IsServerActive { get; }
        IMemRepository<Guid, IAccount> Accounts { get; }
        IAccount ActiveAccount { get; }
        IMethod Method { get; }


        event Action<bool> ReceiveServerActive;

        //Consumer
        event Action<string> ReceiveData;
        event Action<string> ReceiveDataError;


        //Connected
        int ConnectionRetryTimeout { get; }
        bool AutomaticReconnect { get; }
        event Action<bool> ConnectState;
        Task<bool> ConnectAsync();
        void Disconnect();
        bool IsConnect { get; }
        HubConnection HubConnection { get; }
    }
}
