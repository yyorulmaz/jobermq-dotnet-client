using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Imp;
using JoberMQ.Common.Enums.Account;

namespace JoberMQ.Client.DotNet.Factory
{
    internal class AccountFactory
    {
        public static IAccount Create(AccountFactoryEnum accountFactory, bool IsMaster, bool IsActive, string UserName, string Password, IEndpoint EndpointLogin, IEndpoint EndpointHub)
        {
            IAccount account;

            switch (accountFactory)
            {
                case AccountFactoryEnum.Default:
                    account = new AccountDefault(IsMaster, IsActive, UserName, Password, EndpointLogin, EndpointHub);
                    break;
                default:
                    account = new AccountDefault(IsMaster, IsActive, UserName, Password, EndpointLogin, EndpointHub);
                    break;
            }

            return account;
        }
    }
}
