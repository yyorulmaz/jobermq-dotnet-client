using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Abstraction.Connect
{
    public interface IConnect : IDisposable
    {
        int RetryTimeout { get; }
        bool AutoReconnect { get; }

        Task<R> InvokeAsync<R>(string methodName, object arg);

        Task<bool> ConnectAsync();
        bool IsConnect { get; }
        event Action<bool> ConnectState;

        bool IsServerActive { get; }

        event Action<string> ReceiveData;
        event Action<string> ReceiveDataError;
        event Action<bool> ReceiveServerActive;
        event Action<string> ReceiveRpc;
    }
}
