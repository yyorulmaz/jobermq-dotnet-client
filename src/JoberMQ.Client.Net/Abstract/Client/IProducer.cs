using JoberMQ.Client.Common.Models.Builder;
using JoberMQ.Client.Common.Models.Option;
using JoberMQ.Client.Common.Models.Response;

namespace JoberMQ.Client.Net.Abstract.Client
{
    public interface IProducer : IProducerReceive, IDisposable
    {
        public JobBuilderMessageDataModel MessageData(OptionModel option = null);
        public Task<JobDataAddResponseModel> Publish(JobBuilderModel JobBuilder);
    }
}
