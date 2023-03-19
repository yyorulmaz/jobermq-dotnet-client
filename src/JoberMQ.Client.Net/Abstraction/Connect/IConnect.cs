using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Abstraction.Connect
{
    public interface IConnect
    {
        int ConnectionRetryTimeout { get; }
        bool AutomaticReconnect { get; }
        event Action<bool> ConnectState;
        Task<bool> ConnectAsync();
        bool IsConnect { get; }
        HubConnection HubConnection { get; }
        bool IsServerActive { get; }
    }
}
