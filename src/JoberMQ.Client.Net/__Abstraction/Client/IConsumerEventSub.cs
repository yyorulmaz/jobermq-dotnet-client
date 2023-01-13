using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IConsumerEventSub : IDisposable
    {
        public Task<ResponseBaseModel> EventSubscribeClientAsync(string eventName);
        public Task<ResponseBaseModel> EventUnSubscribeClientAsync(string eventName);
        public Task<ResponseBaseModel> EventSubscribeClientGroupKeyAsync(string eventName);
        public Task<ResponseBaseModel> EventUnSubscribeClientGroupKeyAsync(string eventName);
    }
}
