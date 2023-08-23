using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Common.Enums.Endpoint;
using System;

namespace JoberMQ.Client.DotNet.Imp
{
    public class EndpointDefault : IEndpoint
    {
        public EndpointDefault(bool isSsl, string hostName, int port, int portSsl, string action)
        {
            IsSsl = isSsl;
            HostName = hostName;
            Port = port;
            PortSsl = portSsl;
            Action = action;
        }

        public bool IsSsl { get; }
        public string HostName { get; }
        public int Port { get; }
        public int PortSsl { get; }
        public string Action { get; }

        public string GetEndpoint()
        {
            string urlProtocol = IsSsl ? UrlProtocolEnum.https.ToString() : UrlProtocolEnum.http.ToString();
            string portNo = IsSsl ? PortSsl <= 0 ? "" : $":{PortSsl}" : Port <= 0 ? "" : $":{Port}";

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
                disposedValue = true;
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
