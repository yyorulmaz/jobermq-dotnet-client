namespace JoberMQ.Client.Net.Abstract.Client
{
    public interface IConsumer: IConsumerDeclare, IConsumerEventSub, IConsumerReceive, IDisposable
    {
        
    }
}
