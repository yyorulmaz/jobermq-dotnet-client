using JoberMQ.Client.DotNet.Abs;
using System;
using System.Collections.Generic;
using System.Text;

public static class MessageBuilderExtension
{
    public static MessageBuilderModel Message(this IClient client)
        => new MessageBuilderModel(client);
}

public class MessageBuilderModel
{
    internal IClient client { get; set; }
    public Guid transactionId { get; set; }
    internal string key { get; set; }
    internal string message { get; set; }

    public MessageBuilderModel(IClient client)
    {
        this.client = client;
    }
    public MessageBuilderModel(IClient client, string key, string message)
    {
        this.client = client;
        this.key = key;
        this.message = message;
    }
    public MessageBuilderModel(IClient client, Guid transactionId, string key, string message)
    {
        this.client = client;
        this.transactionId = transactionId;
        this.key = key;
        this.message = message;
    }
}
public class FreeMessageClientBuilderModel : MessageBuilderModel
{
    public FreeMessageClientBuilderModel(IClient client, string key, string message) : base(client, key, message)
    {
    }
}
public class FreeMessageGroupBuilderModel : MessageBuilderModel
{
    public FreeMessageGroupBuilderModel(IClient client, string key, string message) : base(client, key, message)
    {
    }
}

public class RpcMessageTextBuilderModel : MessageBuilderModel
{
    public RpcMessageTextBuilderModel(IClient client, Guid transactionId, string key, string message) : base(client, transactionId, key, message)
    {
    }
}
public class RpcMessageFunctionBuilderModel : MessageBuilderModel
{
    public RpcMessageFunctionBuilderModel(IClient client, Guid transactionId, string key, string message) : base(client, transactionId, key, message)
    {
    }
}