using JoberMQ.Client.Net.Models.DeclareConsume;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeBuildExtension
    {
        public static DeclareConsumeBuilderModel Build(this DeclareConsumeBuilderSpecialExtensionModel declareConsumeBuilderSpecialExtension) 
            => declareConsumeBuilderSpecialExtension.DeclareConsumeBuilder;
        public static DeclareConsumeBuilderModel Build(this DeclareConsumeBuilderGroupExtensionModel declareConsumeBuilderGroupExtension)
            => declareConsumeBuilderGroupExtension.DeclareConsumeBuilder;
        public static DeclareConsumeBuilderModel Build(this DeclareConsumeBuilderQueueExtensionModel declareConsumeBuilderQueueExtension)
            => declareConsumeBuilderQueueExtension.DeclareConsumeBuilder;
        public static DeclareConsumeBuilderModel Build(this DeclareConsumeBuilderEventExtensionModel declareConsumeBuilderEventExtension)
            => declareConsumeBuilderEventExtension.DeclareConsumeBuilder;
    }
}
