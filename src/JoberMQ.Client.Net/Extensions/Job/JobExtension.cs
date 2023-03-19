using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Operation;
using JoberMQ.Client.Net.Enums.Publisher;
using JoberMQ.Client.Net.Enums.Timing;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Job;
using JoberMQ.Client.Net.Models.Operation;
using JoberMQ.Client.Net.Models.Publisher;
using JoberMQ.Client.Net.Models.Timing;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobExtension
    {
        public static JobBuilderExtensionModel JobBuilder(this IClient client, InfoModel info = null)
            => Add(client, info);

        private static JobBuilderExtensionModel Add(IClient client, InfoModel info = null)
        {
            var jobBuilderExtension = new JobBuilderExtensionModel();
            jobBuilderExtension.Builder.Operation = new OperationModel { OperationType = OperationTypeEnum.Job } ;
            jobBuilderExtension.Builder.Info = info;
            jobBuilderExtension.Builder.ClientInfo.ClientKey = client.ClientInfo.ClientKey;
            jobBuilderExtension.Builder.ClientInfo.ClientGroupKey = client.ClientInfo.ClientGroupKey;
            jobBuilderExtension.Builder.Publisher = new PublisherModel { PublisherType = PublisherTypeEnum.Standart };
            jobBuilderExtension.Builder.Timing = new TimingModel { TimingType = TimingTypeEnum.Now };
            jobBuilderExtension.Builder.IsResult = false;

            return jobBuilderExtension;
        }
    }
}
