using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Publisher;
using JoberMQ.Common.Models.Job;

namespace JoberMQ.Client.Net.Extension.Job
{
    public static class JobPublisherExtension
    {
        public static JobBuilderPublisherExtensionModel Publisher(this JobBuilderModel jobBuilder, PublisherTypeEnum publisherType = PublisherTypeEnum.Standart)
            => Add(jobBuilder.Job, publisherType);

        private static JobBuilderPublisherExtensionModel Add(JobDbo builder, PublisherTypeEnum publisherType)
        {
            var jJobBuilderPublisherExtension = new JobBuilderPublisherExtensionModel();
            jJobBuilderPublisherExtension.Job = builder;
            jJobBuilderPublisherExtension.Job.Publisher.PublisherType = publisherType;
            return jJobBuilderPublisherExtension;
        }
    }
}
