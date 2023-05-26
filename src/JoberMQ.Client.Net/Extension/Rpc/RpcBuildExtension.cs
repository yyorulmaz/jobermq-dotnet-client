using JoberMQ.Common.Models.Rpc;

namespace JoberMQ.Client.Net.Extension.Rpc
{
    public static class RpcBuildExtension
    {
        public static RpcRequestModel Build(this RpcBuilderMessageExtensionModel jobBuilderMessageExtension) => jobBuilderMessageExtension.RpcMessage;

    }
}
