using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Models.Client;
using JoberMQ.Client.Net.Models.DeclareConsume;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Method.Abstraction;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClient : IDisposable
    {
        ClientInfoModel ClientInfo { get; }
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


        //ConcurrentDictionary<string, DeclareConsumeModel> DeclareConsuming { get; set; }
        ConcurrentDictionary<Guid, DeclareConsumeModel> DeclareConsuming { get; set; }
    }
    public interface IClient2 : IDisposable
    {
        ClientInfoModel2 ClientInfo { get; set; }
        ServerInfoModel2 ServerInfo { get; set; }
        IMemRepository<Guid, IAccount> Accounts { get; }
        IAccount ActiveAccount { get; }
        IMethod Method { get; }
    }
    public class ClientInfoModel2
    {
        public string ClientKey { get; set; }
        public string ClientGroupKey { get; set; }
        bool IsClientActive { get; }
    }
    public class ServerInfoModel2
    {
        bool IsServerActive { get; }
    }





















}
