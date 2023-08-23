using System;

namespace JoberMQ.Client.DotNet.Abs
{
    public interface IEndpoint : IDisposable
    {
        bool IsSsl { get; }
        string HostName { get; }
        int Port { get; }
        int PortSsl { get; }
        string Action { get; }
        string GetEndpoint();
    }
}
