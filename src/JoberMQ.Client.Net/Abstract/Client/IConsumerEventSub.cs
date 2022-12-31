using JoberMQ.Client.Common.Models.Response;

namespace JoberMQ.Client.Net.Abstract.Client
{
    public interface IConsumerEventSub : IDisposable
    {
        public Task<EventSubscribeResponseModel> EventSubscribeClientAsync(string eventName);
        public Task<EventSubscribeResponseModel> EventUnSubscribeClientAsync(string eventName);
        public Task<EventSubscribeResponseModel> EventSubscribeClientGroupKeyAsync(string eventName);
        public Task<EventSubscribeResponseModel> EventUnSubscribeClientGroupKeyAsync(string eventName);
    }
}
