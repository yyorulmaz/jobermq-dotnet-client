using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class QueueExtension
{
    public static async Task<ResponseBaseModel<QueueModel>> QueueGetAsync(this IClient client, string queueKey)
        => await client.Connect.InvokeAsync<ResponseBaseModel<QueueModel>>("QueueGet", queueKey);

    public static async Task<ResponseBaseModel<List<QueueModel>>> QueueGetAllAsync(this IClient client, string queueKey)
        => await client.Connect.InvokeAsync<ResponseBaseModel<List<QueueModel>>>("QueueGetAll", queueKey);

    public static async Task<ResponseBaseModel> QueueAddAsync(this IClient client, string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isActive)
        => await client.Connect.InvokeAsync<ResponseBaseModel>("QueueAdd", new QueueModel(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isActive));

    public static async Task<ResponseBaseModel> QueueEditAsync(this IClient client, string queueKey, string[] tags, PermissionTypeEnum permissionType, bool isDurable, bool isActive)
        => await client.Connect.InvokeAsync<ResponseBaseModel>("QueueEdit", new QueueModel(queueKey, tags, null, null, permissionType, isDurable, isActive));

    public static async Task<ResponseBaseModel> QueueRemoveAsync(this IClient client, string queueKey)
        => await client.Connect.InvokeAsync<ResponseBaseModel>("QueueRemove", queueKey);
}
