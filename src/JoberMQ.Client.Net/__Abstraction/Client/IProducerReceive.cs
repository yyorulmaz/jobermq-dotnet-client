using JoberMQ.Common.Dbos;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IProducerReceive : IDisposable
    {
        public event Action<ErrorMessageDbo> MessageReceiveError;
    }
}
