using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Message;

namespace JoberMQ.Client.Net.Extensions.Message
{
    public static class MessageResultMessageExtension
    {
        public static MessageBuilderResultMessageExtensionModel ResultMessage(this MessageBuilderMessageExtensionModel messageBuilderMessageExtension, IMessage resultMessage)
           => Add(messageBuilderMessageExtension.Message, resultMessage);

        private static MessageBuilderResultMessageExtensionModel Add(MessageDbo builder, IMessage resultMessage = null)
        {
            builder.IsResult = true;

            var message = new MessageModel()
            {
                MessageType = resultMessage.MessageType,
                Message = resultMessage.Message,
                Routing = resultMessage.Routing,
                Info = resultMessage.Info,
                GeneralData = resultMessage.GeneralData,
                PriorityType = resultMessage.PriorityType
            };
            builder.ResultMessage = message;

            return new MessageBuilderResultMessageExtensionModel { Message = builder };
        }
    }
}
