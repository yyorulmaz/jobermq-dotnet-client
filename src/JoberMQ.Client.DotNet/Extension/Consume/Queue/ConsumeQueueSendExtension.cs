using JoberMQ.Common.Models.Base;
using System.Threading.Tasks;

public static class ConsumeQueueSendExtension
{
    public static async Task<ResponseBaseModel> SendAsync(this ConsumeQueueSubBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel>("ConsumeQueueSub", builder.clientKey, builder.queueKey, builder.isDurable);

    public static async Task<ResponseBaseModel> SendAsync(this ConsumeQueueUnSubBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel>("ConsumeQueueUnSub", builder.clientKey, builder.queueKey);
}