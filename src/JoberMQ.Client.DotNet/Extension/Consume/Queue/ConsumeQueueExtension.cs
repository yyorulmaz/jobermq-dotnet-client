public static class ConsumeQueueExtension
{
    public static ConsumeQueueSubBuilderModel QueueSub(this ConsumeBuilderModel builder, string queueKey, bool isDurable)
        => new ConsumeQueueSubBuilderModel(builder.client, builder.client.ClientInfo.ClientKey, queueKey, isDurable);

    public static ConsumeQueueUnSubBuilderModel QueueUnSub(this ConsumeBuilderModel builder, string queueKey)
        => new ConsumeQueueUnSubBuilderModel(builder.client, builder.client.ClientInfo.ClientKey, queueKey);
}
