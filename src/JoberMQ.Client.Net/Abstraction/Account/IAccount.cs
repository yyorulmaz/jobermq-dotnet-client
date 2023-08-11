using JoberMQ.Client.Net.Abstraction.Endpoint;
using System;

namespace JoberMQ.Client.Net.Abstraction.Account
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
