using JoberMQ.Common.Method.Abstraction;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet.Abs
{
    public interface IClient : IDisposable
    {
        IClientInfo ClientInfo { get; }
        Task<bool> ConnectAsync();
        event Action<string> ReceiveFreeMessageText;
        event Action<Guid, string> ReceiveRpcMessageText;
        Task<R> InvokeAsync<R>(string methodName, object arg1, object arg2);
        Task<R> InvokeAsync<R>(string methodName, object arg1, object arg2, object arg3);
        Task SendAsync(string methodName, object arg1);
        Task SendAsync(string methodName, object arg1, object arg2);
        Task SendAsync(string methodName, object arg1, object arg2, object arg3, object arg4);
        IMethod Method { get; }
    }
}
