using System;

namespace JoberMQ.Client.DotNet.Abs
{
    internal interface IAccount : IDisposable
    {
        bool IsMaster { get; }
        bool IsActive { get; }
        string UserName { get; }
        string Password { get; }
        string Token { get; set; }
        IEndpoint EndpointLogin { get; }
        IEndpoint EndpointHub { get; }
    }
}
