using JoberMQ.Client.Common.Models.Response;

namespace JoberMQ.Client.Net.Abstract.Client
{
    public interface IConsumerDeclare : IDisposable
    {
        public Task<DeclareConsumeResponseModel> DeclareConsumeSpecialAddAsync();
        public Task<DeclareConsumeResponseModel> DeclareConsumeSpecialRemoveAsync();
        public Task<DeclareConsumeResponseModel> DeclareConsumeGroupAddAsync();
        public Task<DeclareConsumeResponseModel> DeclareConsumeGroupRemoveAsync();
        public Task<DeclareConsumeResponseModel> DeclareConsumeQueueKeyAddAsync(string queueName, string key);
        public Task<DeclareConsumeResponseModel> DeclareConsumeQueueKeyRemoveAsync(string queueName, string key);
        
        public Task<DeclareConsumeResponseModel> DeclareConsumeErrorSpecialAddAsync();
        public Task<DeclareConsumeResponseModel> DeclareConsumeErrorSpecialRemoveAsync();
        public Task<DeclareConsumeResponseModel> DeclareConsumeErrorGroupAddAsync();
        public Task<DeclareConsumeResponseModel> DeclareConsumeErrorGroupRemoveAsync();
        public Task<DeclareConsumeResponseModel> DeclareConsumeErrorQueueKeyAddAsync(string queueName, string key);
        public Task<DeclareConsumeResponseModel> DeclareConsumeErrorQueueKeyRemoveAsync(string queueName, string key);
    }
}
