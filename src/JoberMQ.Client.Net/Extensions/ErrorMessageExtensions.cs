using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Models.Builder;

namespace JoberMQ.Client.Net.Extensions
{
    public static class ErrorMessageExtensions
    {
        public static JobBuilderErrorMessageModel ErrorMessageSpecial(this JobBuilderMessageDataModel builderData)
            => ErrorMessageAdd(builderData.JobBuilder, RoutingTypeEnum.Special);
        public static JobBuilderErrorMessageModel ErrorMessageSpecial(this JobBuilderTimingModel builderData)
            => ErrorMessageAdd(builderData.JobBuilder, RoutingTypeEnum.Special);
        public static JobBuilderErrorMessageModel ErrorMessageSpecial(this JobBuilderRunModel builderData)
            => ErrorMessageAdd(builderData.JobBuilder, RoutingTypeEnum.Special);
        public static JobBuilderErrorMessageModel ErrorMessageGroup(this JobBuilderMessageDataModel builderData)
            => ErrorMessageAdd(builderData.JobBuilder, RoutingTypeEnum.Group);
        public static JobBuilderErrorMessageModel ErrorMessageGroup(this JobBuilderTimingModel builderData)
            => ErrorMessageAdd(builderData.JobBuilder, RoutingTypeEnum.Group);
        public static JobBuilderErrorMessageModel ErrorMessageGroup(this JobBuilderRunModel builderData)
            => ErrorMessageAdd(builderData.JobBuilder, RoutingTypeEnum.Group);


        private static JobBuilderErrorMessageModel ErrorMessageAdd(JobBuilderModel builderData, RoutingTypeEnum errorMessageRoutingType)
        {
            var builder = new JobBuilderErrorMessageModel();
            builder.JobBuilder = builderData;
            builder.JobBuilder.ErrorMessageRoutingType = errorMessageRoutingType;
            return builder;
        }
    }
}
