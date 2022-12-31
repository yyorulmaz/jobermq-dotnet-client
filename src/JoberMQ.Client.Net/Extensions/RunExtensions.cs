using JoberMQ.Client.Common.Enums.Run;
using JoberMQ.Client.Common.Models.Builder;

namespace JoberMQ.Client.Net.Extensions
{
    public static class RunExtensions
    {
        public static JobBuilderRunModel Run(this JobBuilderMessageDataModel builderData, RunTypeEnum runType = RunTypeEnum.Standart)
            => Add(builderData.JobBuilder, runType);
        public static JobBuilderRunModel Run(this JobBuilderTimingModel builderData, RunTypeEnum runType = RunTypeEnum.Standart)
            => Add(builderData.JobBuilder, runType);

        private static JobBuilderRunModel Add(JobBuilderModel builderData, RunTypeEnum runType = RunTypeEnum.Standart)
        {
            var builder = new JobBuilderRunModel();
            builder.JobBuilder = builderData;
            builder.JobBuilder.JobRunType = runType;
            return builder;
        }
    }
}
