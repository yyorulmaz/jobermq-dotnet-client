﻿using JoberMQ.Common.Models.Rpc;

public static class RpcBuildExtension
{
    public static RpcRequestModel Build(this RpcBuilderMessageExtensionModel jobBuilderMessageExtension) => jobBuilderMessageExtension.RpcMessage;

}
