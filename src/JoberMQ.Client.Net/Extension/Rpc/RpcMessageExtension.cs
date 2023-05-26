using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Common.Models.Rpc;

namespace JoberMQ.Client.Net.Extension.Rpc
{
    public static class RpcMessageExtension
    {
        public static RpcBuilderMessageExtensionModel Message(this RpcBuilderModel rpcBuilder, IMessageRpc messageRpc)
            => Add(rpcBuilder.RpcMessage, messageRpc);

        private static RpcBuilderMessageExtensionModel Add(RpcRequestModel builder, IMessageRpc messageRpc)
        {
            builder.ConsumerId = messageRpc.ConsumerId;
            builder.MessageType = messageRpc.MessageType;
            builder.Message = messageRpc.Message;

            return new RpcBuilderMessageExtensionModel { RpcMessage = builder };
        }
    }
}
