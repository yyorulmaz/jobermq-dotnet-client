using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class QueueSendExtension
{
    public static async Task<ResponseBaseModel> SendAsync(this QueueAddBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel>("QueueAdd", builder.GetQueueModel());

    public static async Task<ResponseBaseModel<QueueModel>> SendAsync(this QueueGetBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel<QueueModel>>("QueueGet", builder.queueKey);

    public static async Task<ResponseBaseModel<List<QueueModel>>> SendAsync(this QueueGetAllBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel<List<QueueModel>>>("QueueGetAll");

    public static async Task<ResponseBaseModel> SendAsync(this QueueEditBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel>("QueueEdit", builder.GetQueueModel());

    public static async Task<ResponseBaseModel> SendAsync(this QueueRemoveBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel>("QueueRemove", builder.queueKey);
}