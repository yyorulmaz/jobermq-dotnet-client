using JoberMQ.Common.Models.Response;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IConsumerEventSub : IDisposable
    {
        public Task<EventSubscribeResponseModel> EventSubscribeClientAsync(string eventName);
        public Task<EventSubscribeResponseModel> EventUnSubscribeClientAsync(string eventName);
        public Task<EventSubscribeResponseModel> EventSubscribeClientGroupKeyAsync(string eventName);
        public Task<EventSubscribeResponseModel> EventUnSubscribeClientGroupKeyAsync(string eventName);
    }
}
