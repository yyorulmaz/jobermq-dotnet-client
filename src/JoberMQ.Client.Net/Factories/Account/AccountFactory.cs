using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Implementation.Account.Default;
using JoberMQ.Library.Enums.Account;

namespace JoberMQ.Client.Net.Factories.Account
{
    internal class AccountFactory
    {
        public static IAccount Create(AccountFactoryEnum accountFactory, bool IsMaster, bool IsActive, string UserName, string Password, IEndpoint EndpointLogin, IEndpoint EndpointHub)
        {
            IAccount account;

            switch (accountFactory)
            {
                case AccountFactoryEnum.Default:
                    account = new DfAccount(IsMaster, IsActive, UserName, Password, EndpointLogin, EndpointHub);
                    break;
                default:
                    account = new DfAccount(IsMaster, IsActive, UserName, Password, EndpointLogin, EndpointHub);
                    break;
            }

            return account;
        }
    }
}
