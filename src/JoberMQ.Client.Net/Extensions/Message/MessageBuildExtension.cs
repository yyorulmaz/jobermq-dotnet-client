using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models.Message;

namespace JoberMQ.Client.Net.Extensions.Message
{
    public static class MessageBuildExtension
    {
        public static MessageDbo Build(this MessageBuilderMessageExtensionModel messageBuilderMessage) => messageBuilderMessage.Message;
        public static MessageDbo Build(this MessageBuilderResultMessageExtensionModel messageBuilderResultMessage) => messageBuilderResultMessage.Message;
    }
}
