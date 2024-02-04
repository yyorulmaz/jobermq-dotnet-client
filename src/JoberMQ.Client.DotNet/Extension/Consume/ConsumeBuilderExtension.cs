using JoberMQ.Client.DotNet.Abs;

public static class ConsumeBuilderExtension
{
    public static ConsumeBuilderModel Consume(this IClient client)
            => new ConsumeBuilderModel(client);
}

public class ConsumeBuilderModel
{
    internal IClient client { get; set; }
    internal ConsumeBuilderModel(IClient client)
        => this.client = client;
}

public class ConsumeQueueSubBuilderModel
{
    internal IClient client { get; set; }
    internal string clientKey { get; set; }
    internal string queueKey { get; set; }
    internal bool isDurable { get; set; }
    internal ConsumeQueueSubBuilderModel(IClient client, string clientKey, string queueKey, bool isDurable)
    {
        this.client = client;
        this.clientKey = clientKey;
        this.queueKey = queueKey;
        this.isDurable = isDurable;
    }
}

public class ConsumeQueueUnSubBuilderModel
{
    internal IClient client { get; set; }
    internal string clientKey { get; set; }
    internal string queueKey { get; set; }
    internal ConsumeQueueUnSubBuilderModel(IClient client, string clientKey, string queueKey)
    {
        this.client = client;
        this.clientKey = clientKey;
        this.queueKey = queueKey;
    }
}

public class ConsumeMessageFreeGroupSubBuilderModel
{
    internal IClient client { get; set; }
    internal string groupKey { get; set; }
    internal ConsumeMessageFreeGroupSubBuilderModel(IClient client, string groupKey)
    {
        this.client = client;
        this.groupKey = groupKey;
    }
}

public class ConsumeMessageFreeGroupUnSubBuilderModel
{
    internal IClient client { get; set; }
    internal string groupKey { get; set; }
    internal ConsumeMessageFreeGroupUnSubBuilderModel(IClient client, string groupKey)
    {
        this.client = client;
        this.groupKey = groupKey;
    }
}
