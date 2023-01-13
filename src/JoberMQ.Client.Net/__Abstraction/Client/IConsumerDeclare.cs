using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IConsumerDeclare : IDisposable
    {
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
    }
}
