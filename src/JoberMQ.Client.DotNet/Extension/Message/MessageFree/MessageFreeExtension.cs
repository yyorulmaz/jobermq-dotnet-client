public static class MessageFreeExtension
{
    public static MessageFreeClientBuilderModel FreeClient(this MessageBuilderModel messageBuilder, string clientKey, string message)
        => new MessageFreeClientBuilderModel(messageBuilder.client, clientKey, message);
    public static MessageFreeGroupBuilderModel FreeGroup(this MessageBuilderModel messageBuilder, string groupKey, string message)
        => new MessageFreeGroupBuilderModel(messageBuilder.client, groupKey, message);
}