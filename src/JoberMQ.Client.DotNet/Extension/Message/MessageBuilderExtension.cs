using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Common.Dbos;
using System;

public static class MessageBuilderExtension
{
    public static MessageBuilderModel Message(this IClient client)
        => new MessageBuilderModel(client);
}

public class MessageBuilderModel
{
    internal IClient client { get; set; }
    internal MessageBuilderModel(IClient client)
        => this.client = client;
}

public class MessageFreeClientBuilderModel
{
    internal IClient client { get; set; }
    internal string key { get; set; }
    internal string message { get; set; }
    public MessageFreeClientBuilderModel(IClient client, string key, string message)
    {
        this.client = client;
        this.key = key;
        this.message = message;
    }
}

public class MessageFreeGroupBuilderModel
{
    internal IClient client { get; set; }
    internal string key { get; set; }
    internal string message { get; set; }
    public MessageFreeGroupBuilderModel(IClient client, string key, string message)
    {
        this.client = client;
        this.key = key;
        this.message = message;
    }
}

public class MessageRpcTextBuilderModel
{
    internal IClient client { get; set; }
    public Guid transactionId { get; set; }
    internal string key { get; set; }
    internal string message { get; set; }
    public MessageRpcTextBuilderModel(IClient client, Guid transactionId, string key, string message)
    {
        this.client = client;
        this.transactionId = transactionId;
        this.key = key;
        this.message = message;
    }
}

public class MessageRpcFunctionBuilderModel
{
    internal IClient client { get; set; }
    public Guid transactionId { get; set; }
    internal string key { get; set; }
    internal string message { get; set; }
    public MessageRpcFunctionBuilderModel(IClient client, Guid transactionId, string key, string message)
    {
        this.client = client;
        this.transactionId = transactionId;
        this.key = key;
        this.message = message;
    }
}




public class MessageMessageBuilderModel
{
    internal IClient client { get; set; }
    internal MessageDbo messageDbo { get; set; }
    public MessageMessageBuilderModel(IClient client, MessageDbo messageDbo)
    {
        this.client = client;
        this.messageDbo = messageDbo;
    }
}
public class MessageMessageResultBuilderModel
{
    internal IClient client { get; set; }
    internal MessageDbo messageDbo { get; set; }
    public MessageMessageResultBuilderModel(IClient client, MessageDbo messageDbo)
    {
        this.client = client;
        this.messageDbo = messageDbo;
    }
}






