using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using System;

namespace JoberMQ.Client.Net.Abstraction.Account
{
    public interface IAccountInfo
    {
        IMemRepository<Guid, IAccount> Accounts { get; }
        IAccount ActiveAccount { get; }
    }
}
