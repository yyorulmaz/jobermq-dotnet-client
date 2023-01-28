using JoberMQ.Client.Net.Enums.Publisher;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Job;
using JoberMQ.Client.Net.Models.Publisher;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobPublisherExtension
    {
        public static JobBuilderPublisherExtensionModel Publisher(this JobBuilderExtensionModel jobBuilderExtension, PublisherTypeEnum publisherType = PublisherTypeEnum.Standart)
            => Add(jobBuilderExtension.Builder, publisherType);

        private static JobBuilderPublisherExtensionModel Add(JobBuilderModel builder, PublisherTypeEnum publisherType)
        {
            var jJobBuilderPublisherExtension = new JobBuilderPublisherExtensionModel();
            jJobBuilderPublisherExtension.Builder = builder;
            jJobBuilderPublisherExtension.Builder.Publisher = new PublisherModel { PublisherType = publisherType };
            return jJobBuilderPublisherExtension;
        }
    }
}
