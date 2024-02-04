using JoberMQ.Common.Models.Response;
using System.Threading.Tasks;

public static class MessageMessageSendExtension
{
    public static async Task<ResponseModel> SendAsync(this MessageMessageBuilderModel builder)
      => await builder.client.InvokeAsync<ResponseModel>("MessageMessage", builder.messageDbo);
    public static async Task<ResponseModel> SendAsync(this MessageMessageResultBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseModel>("MessageMessage", builder.messageDbo);
}
