using JoberMQ.Library.Models.Message;
using JoberMQ.Library.Models.Rpc;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Extensions.Rpc
{
    public static class RpcBuildExtension
    {
        public static RpcRequestModel Build(this RpcBuilderMessageExtensionModel jobBuilderMessageExtension) => jobBuilderMessageExtension.RpcMessage;

    }
}
