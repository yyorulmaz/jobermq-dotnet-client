using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Library.Enums.Endpoint;
using System;

namespace JoberMQ.Client.Net.Implementation.Endpoint.Default
{
    public class DfEndpoint : IEndpoint
    {
        public DfEndpoint(bool IsSsl, string HostName, int Port, int PortSsl, string Action)
        {
            this.IsSsl = IsSsl;
            this.HostName = HostName;
            this.Port = Port;
            this.PortSsl = PortSsl;
            this.Action = Action;
        }

        public bool IsSsl { get; }
        public string HostName { get; }
        public int Port { get; }
        public int PortSsl { get; }
        public string Action { get; }

        public string GetEndpoint()
        {
            var urlProtocol = IsSsl == true ? UrlProtocolEnum.https.ToString() : UrlProtocolEnum.http.ToString();

            string portNo;
            if (IsSsl)
                portNo = PortSsl <= 0 ? "" : $":{PortSsl}";
            else
                portNo = Port <= 0 ? "" : $":{Port}";

            return $"{urlProtocol}://{HostName}{portNo}/{Action}";
        }

        #region Dispose
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue=true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DfEndpoint()
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
