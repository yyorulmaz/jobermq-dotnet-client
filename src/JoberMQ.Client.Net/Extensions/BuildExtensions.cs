using JoberMQ.Client.Common.Models.Builder;

namespace JoberMQ.Client.Net.Extensions
{
    public static class BuildExtensions
    {
        public static JobBuilderModel Build(this JobBuilderMessageDataModel builderData) => builderData.JobBuilder;
        public static JobBuilderModel Build(this JobBuilderTimingModel builderData) => builderData.JobBuilder;
        public static JobBuilderModel Build(this JobBuilderRunModel builderData) => builderData.JobBuilder;
        public static JobBuilderModel Build(this JobBuilderErrorMessageModel builderData) => builderData.JobBuilder;
    }
}
