using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Factories.Account;
using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using System;
using System.Configuration;
using System.Security.Principal;

namespace JoberMQ.Client.Net.Implementation.Account.Default
{
    public class DfAccountInfo : IAccountInfo
    {
        public DfAccountInfo(IConfiguration configuration, IEndpointDetail endpointDetail)
        {
            accounts = MemFactory.Create<Guid, IAccount>(MemFactoryEnum.Default, MemDataFactoryEnum.None);
            var account = AccountFactory.Create(configuration, true, true, endpointDetail);
            accounts.Add(Guid.NewGuid(), account);

        }
        IMemRepository<Guid, IAccount> accounts;
        public IMemRepository<Guid, IAccount> Accounts => accounts;

        IAccount activeAccount;
        public IAccount ActiveAccount => activeAccount;
    }
}
