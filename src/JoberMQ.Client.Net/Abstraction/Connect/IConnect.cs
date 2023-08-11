using JoberMQ.Common.Dbos;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Abstraction.Connect
{
    public interface IConnect : IDisposable
    {
        int RetryTimeout { get; }
        bool AutoReconnect { get; }

        Task<R> InvokeAsync<R>(string methodName, object arg);
        Task<R> InvokeAsync<R>(string methodName, object arg1, object arg2);
        Task<R> InvokeAsync<R>(string methodName, object arg1, object arg2, object arg3);

        Task SendAsync(string methodName, object arg);
        Task SendAsync(string methodName, object arg1, object arg2);
        Task SendAsync(string methodName, object arg1, object arg2, object arg3);

        Task<bool> ConnectAsync();
        bool IsConnect { get; }
        event Action<bool> ConnectState;

        bool IsServerActive { get; }

        //event Action<string> ReceiveData; 
        event Action<MessageDbo> ReceiveData; 
        event Action<string> ReceiveDataText;
        event Action<string> ReceiveDataError;
        event Action<bool> ReceiveServerActive;
        event Action<string> ReceiveRpc;
    }
}
