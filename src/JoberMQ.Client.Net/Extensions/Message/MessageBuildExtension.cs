using JoberMQ.Client.Net.Models.Builder;

namespace JoberMQ.Client.Net.Extensions.Message
{
    public static class MessageBuildExtension
    {
        public static BuilderModel Build(this MessageBuilderMessageModel messageBuilderMessage) => messageBuilderMessage.Builder;
        public static BuilderModel Build(this MessageBuilderResultMessageModel messageBuilderResultMessage) => messageBuilderResultMessage.Builder;
    }
}
