using JoberMQ.Client.Common.Dbos;

namespace JoberMQ.Client.Net.Abstract.Client
{
    public interface IProducerReceive : IDisposable
    {
        public event Action<ErrorMessageDbo> MessageReceiveError;
    }
}
