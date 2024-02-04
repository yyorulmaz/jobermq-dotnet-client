using JoberMQ.Common.Dbos;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet.Abs
{
    public interface IConnect : IDisposable
    {
        HubConnection HubConn { get; }
        Task<bool> ConnectAsync();

        bool IsConnect { get; }
        event Action<bool> ConnectState;

        bool IsServerActive { get; }
        event Action<bool> ServerActiveState;

        event Action<string> ReceiveMessageFreeText;
        event Action<Guid, string> ReceiveMessageRpcText;
        event Action<Guid, string> ReceiveMessageRpcFunction;
        event Action<MessageDbo> ReceiveMessage;
    }
}
