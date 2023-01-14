using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Declare;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IConsumer: IDisposable
    {
        public DeclareConsumeModel DeclareConsume();

        public Task<ResponseBaseModel> DeclareConsumeSpecialAddAsync();
        public Task<ResponseBaseModel> DeclareConsumeSpecialRemoveAsync();
        public Task<ResponseBaseModel> DeclareConsumeGroupAddAsync();
        public Task<ResponseBaseModel> DeclareConsumeGroupRemoveAsync();
        public Task<ResponseBaseModel> DeclareConsumeQueueKeyAddAsync(string queueName, string key);
        public Task<ResponseBaseModel> DeclareConsumeQueueKeyRemoveAsync(string queueName, string key);

        public Task<ResponseBaseModel> DeclareConsumeErrorSpecialAddAsync();
        public Task<ResponseBaseModel> DeclareConsumeErrorSpecialRemoveAsync();
        public Task<ResponseBaseModel> DeclareConsumeErrorGroupAddAsync();
        public Task<ResponseBaseModel> DeclareConsumeErrorGroupRemoveAsync();
        public Task<ResponseBaseModel> DeclareConsumeErrorQueueKeyAddAsync(string queueName, string key);
        public Task<ResponseBaseModel> DeclareConsumeErrorQueueKeyRemoveAsync(string queueName, string key);




        public Task<ResponseBaseModel> EventSubscribeClientAsync(string eventName);
        public Task<ResponseBaseModel> EventUnSubscribeClientAsync(string eventName);
        public Task<ResponseBaseModel> EventSubscribeClientGroupKeyAsync(string eventName);
        public Task<ResponseBaseModel> EventUnSubscribeClientGroupKeyAsync(string eventName);



        public event Action<string> MessageReceive;
        public bool TextMessageReceiveAutoCompleted { get; }
    }
}
