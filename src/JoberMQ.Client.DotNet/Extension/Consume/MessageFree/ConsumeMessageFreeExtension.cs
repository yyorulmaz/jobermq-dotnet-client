public static class ConsumeMessageFreeExtension
{
    public static ConsumeMessageFreeGroupSubBuilderModel MessageFreeGroupSub(this ConsumeBuilderModel builder, string groupKey)
        => new ConsumeMessageFreeGroupSubBuilderModel(builder.client, groupKey);

    public static ConsumeMessageFreeGroupUnSubBuilderModel MessageFreeGroupUnSub(this ConsumeBuilderModel builder, string groupKey)
        => new ConsumeMessageFreeGroupUnSubBuilderModel(builder.client, groupKey);
}
