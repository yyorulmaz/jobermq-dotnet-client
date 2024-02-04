using System.Threading.Tasks;

public static class ConsumeMessageFreeSendExtension
{
    public static async Task SendAsync(this ConsumeMessageFreeGroupSubBuilderModel builder)
        => await builder.client.SendAsync("ConsumeMessageFreeGroupSub", builder.groupKey);

    public static async Task SendAsync(this ConsumeMessageFreeGroupUnSubBuilderModel builder)
        => await builder.client.SendAsync("ConsumeMessageFreeGroupUnSub", builder.groupKey);

}