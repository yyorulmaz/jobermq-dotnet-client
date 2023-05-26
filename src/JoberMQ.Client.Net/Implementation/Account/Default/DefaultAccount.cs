using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Endpoint;
using System;

namespace JoberMQ.Client.Net.Implementation.Account.Default
{
    internal class DefaultAccount : IAccount
    {
        public DefaultAccount(bool isMaster, bool isActive, string userName, string password, IEndpoint endpointLogin, IEndpoint endpointHub)
        {
            IsMaster = isMaster;
            IsActive = isActive;
            UserName = userName;
            Password = password;
            EndpointLogin = endpointLogin;
            EndpointHub = endpointHub;
        }

        public bool IsMaster { get; }
        public bool IsActive { get; }
        public string UserName { get; }
        public string Password { get; }
        public string Token { get; set; }
        public IEndpoint EndpointLogin { get; }
        public IEndpoint EndpointHub { get; }

        #region Dispose
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    EndpointLogin.Dispose();
                    EndpointHub.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DfAccount()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
