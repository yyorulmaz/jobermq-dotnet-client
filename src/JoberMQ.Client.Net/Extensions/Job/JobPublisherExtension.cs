using JoberMQ.Client.Net.Enums.Publisher;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Publisher;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobPublisherExtension
    {
        public static JobBuilderPublisherModel Publisher(this JobBuilderModel jobBuilder, PublisherTypeEnum publisherType = PublisherTypeEnum.Standart)
            => Add(jobBuilder.Builder, publisherType);

        private static JobBuilderPublisherModel Add(BuilderModel builder, PublisherTypeEnum publisherType)
        {
            var jJobBuilderPublisher = new JobBuilderPublisherModel();
            jJobBuilderPublisher.Builder = builder;
            jJobBuilderPublisher.Builder.Publisher = new PublisherModel { PublisherType = publisherType };
            return jJobBuilderPublisher;
        }
    }
}
