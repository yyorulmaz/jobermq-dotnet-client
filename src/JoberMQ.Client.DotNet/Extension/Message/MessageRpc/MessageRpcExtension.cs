using System.Linq.Expressions;
using System;

public static class MessageRpcExtension
{
    public static MessageRpcTextBuilderModel Rpc(this MessageBuilderModel messageBuilder, string consumerKey, string message)
        => new MessageRpcTextBuilderModel(messageBuilder.client, Guid.NewGuid(), consumerKey, message);
    public static MessageRpcFunctionBuilderModel Rpc(this MessageBuilderModel messageBuilder, string consumerKey, Expression<Action> methodCall)
        => new MessageRpcFunctionBuilderModel(messageBuilder.client, Guid.NewGuid(), consumerKey, messageBuilder.client.Method.MethodPropertySerialize(methodCall));

}