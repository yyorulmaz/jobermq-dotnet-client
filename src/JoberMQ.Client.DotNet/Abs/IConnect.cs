using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet.Abs
{
    internal interface IConnect : IDisposable
    {
        HubConnection HubConn { get; }
        Task<bool> ConnectAsync();

        bool IsConnect { get; }
        event Action<bool> ConnectState;

        bool IsServerActive { get; }
        event Action<bool> ServerActiveState;

        event Action<string> ReceiveFreeMessageText;
        event Action<Guid, string> ReceiveRpcMessageText;
        event Action<Guid, string> ReceiveRpcMessageFunction;
    }
}
