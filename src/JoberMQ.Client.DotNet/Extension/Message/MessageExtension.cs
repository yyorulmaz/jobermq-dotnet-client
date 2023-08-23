using System.Linq.Expressions;
using System;

public static class MessageExtension
{
    #region FREE MESSAGE
    public static FreeMessageClientBuilderModel FreeMessageClient(this MessageBuilderModel messageBuilder, string clientKey, string message)
        => new FreeMessageClientBuilderModel(messageBuilder.client, clientKey, message);
    public static FreeMessageGroupBuilderModel FreeMessageGroup(this MessageBuilderModel messageBuilder, string groupKey, string message)
        => new FreeMessageGroupBuilderModel(messageBuilder.client, groupKey, message);
    #endregion

    #region RPC
    public static RpcMessageTextBuilderModel RpcMessage(this MessageBuilderModel messageBuilder, string consumerKey, string message)
        => new RpcMessageTextBuilderModel(messageBuilder.client, Guid.NewGuid(), consumerKey, message);
    public static RpcMessageFunctionBuilderModel RpcMessage(this MessageBuilderModel messageBuilder, string consumerKey, Expression<Action> methodCall)
        => new RpcMessageFunctionBuilderModel(messageBuilder.client, Guid.NewGuid(), consumerKey, messageBuilder.client.Method.MethodPropertySerialize(methodCall));
    #endregion

}