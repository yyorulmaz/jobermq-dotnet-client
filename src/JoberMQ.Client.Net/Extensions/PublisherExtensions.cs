using JoberMQ.Common.Enums.Publisher;
using JoberMQ.Common.Models.Builder;

namespace JoberMQ.Client.Net.Extensions
{
    public static class PublisherExtensions
    {
        public static JobBuilderRunModel Run(this JobBuilderMessageDataModel builderData, PublisherTypeEnum runType = PublisherTypeEnum.Standart)
            => Add(builderData.JobBuilder, runType);
        public static JobBuilderRunModel Run(this JobBuilderTimingModel builderData, PublisherTypeEnum runType = PublisherTypeEnum.Standart)
            => Add(builderData.JobBuilder, runType);

        private static JobBuilderRunModel Add(JobBuilderModel builderData, PublisherTypeEnum runType = PublisherTypeEnum.Standart)
        {
            var builder = new JobBuilderRunModel();
            builder.JobBuilder = builderData;
            builder.JobBuilder.PublisherType = runType;
            return builder;
        }
    }
}
