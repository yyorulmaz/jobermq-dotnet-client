using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Operation;
using JoberMQ.Client.Net.Enums.Publisher;
using JoberMQ.Client.Net.Enums.Timing;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Operation;
using JoberMQ.Client.Net.Models.Publisher;
using JoberMQ.Client.Net.Models.Timing;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobExtension
    {
        public static JobBuilderModel JobBuilder(this IClient client, InfoModel info = null)
            => Add(client, info);

        private static JobBuilderModel Add(IClient client, InfoModel info = null)
        {
            var jobBuilder = new JobBuilderModel();
            jobBuilder.Builder.Operation = new OperationModel { OperationType = OperationTypeEnum.Job } ;
            jobBuilder.Builder.Info = info;
            jobBuilder.Builder.Producer = client.Producer;
            jobBuilder.Builder.Publisher = new PublisherModel { PublisherType = PublisherTypeEnum.Standart };
            jobBuilder.Builder.Timing = new TimingModel { TimingType = TimingTypeEnum.Now };
            jobBuilder.Builder.IsResult = false;

            return jobBuilder;
        }
    }
}
