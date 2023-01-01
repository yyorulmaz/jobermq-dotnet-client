namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IConsumer: IConsumerDeclare, IConsumerEventSub, IConsumerReceive, IDisposable
    {
        
    }
}
