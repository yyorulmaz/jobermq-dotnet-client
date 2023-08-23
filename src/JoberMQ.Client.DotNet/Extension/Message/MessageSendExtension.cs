using JoberMQ.Common.Models.Rpc;
using System.Threading.Tasks;

public static class MessageSendExtension
{
    public static async Task SendAsync(this FreeMessageClientBuilderModel builder)
        => await builder.client.SendAsync("FreeMessageClient", builder.key, builder.message);
    public static async Task SendAsync(this FreeMessageGroupBuilderModel builder)
        => await builder.client.SendAsync("FreeMessageGroup", builder.key, builder.message);

    public static async Task<RpcResponseModel> SendAsync(this RpcMessageTextBuilderModel builder)
        => await builder.client.InvokeAsync<RpcResponseModel>("RpcMessageText", builder.transactionId, builder.key, builder.message);
    public static async Task<RpcResponseModel> SendAsync(this RpcMessageFunctionBuilderModel builder)
        => await builder.client.InvokeAsync<RpcResponseModel>("RpcMessageFunction", builder.transactionId, builder.key, builder.message);
}
