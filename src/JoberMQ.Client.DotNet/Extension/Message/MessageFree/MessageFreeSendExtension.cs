using System.Threading.Tasks;

public static class MessageFreeSendExtension
{
    public static async Task SendAsync(this MessageFreeClientBuilderModel builder)
        => await builder.client.SendAsync("MessageFreeClient", builder.key, builder.message);
    public static async Task SendAsync(this MessageFreeGroupBuilderModel builder)
        => await builder.client.SendAsync("MessageFreeGroup", builder.key, builder.message);
}
