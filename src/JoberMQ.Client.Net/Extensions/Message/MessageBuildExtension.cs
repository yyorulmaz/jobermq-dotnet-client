using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.MessageBuilder;

namespace JoberMQ.Client.Net.Extensions.Message
{
    public static class MessageBuildExtension
    {
        public static MessageBuilderModel Build(this MessageBuilderMessageExtensionModel messageBuilderMessage) => messageBuilderMessage.Builder;
        public static MessageBuilderModel Build(this MessageBuilderResultMessageExtensionModel messageBuilderResultMessage) => messageBuilderResultMessage.Builder;
    }
}
