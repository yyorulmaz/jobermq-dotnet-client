using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Builder;
using JoberMQ.Common.Models.Option;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IProducer : IDisposable
    {
        public JobBuilderMessageDataModel MessageData(OptionModel option = null);
        public Task<JobDataAddResponseModel> Publish(JobBuilderModel JobBuilder);



        public event Action<ErrorMessageDbo> MessageReceiveError;
    }
}
