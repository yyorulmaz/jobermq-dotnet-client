using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class QueueExtension
{
    public static QueueBuilderModel Queue(this IClient client)
        => new QueueBuilderModel(ref client);


    public static async Task<ResponseBaseModel> AddAsync(this QueueBuilderModel queueBuilder, string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isActive)
        => await queueBuilder.client.Connect.InvokeAsync<ResponseBaseModel>("QueueAdd", new QueueModel(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isActive));
    public static async Task<ResponseBaseModel<QueueModel>> GetAsync(this QueueBuilderModel queueBuilder, string queueKey)
        => await queueBuilder.client.Connect.InvokeAsync<ResponseBaseModel<QueueModel>>("QueueGet", queueKey);
    public static async Task<ResponseBaseModel<List<QueueModel>>> GetAllAsync(this QueueBuilderModel queueBuilder, string queueKey)
        => await queueBuilder.client.Connect.InvokeAsync<ResponseBaseModel<List<QueueModel>>>("QueueGetAll", queueKey);
    public static async Task<ResponseBaseModel> EditAsync(this QueueBuilderModel queueBuilder, string queueKey, string[] tags, PermissionTypeEnum permissionType, bool isDurable, bool isActive)
        => await queueBuilder.client.Connect.InvokeAsync<ResponseBaseModel>("QueueEdit", new QueueModel(queueKey, tags, null, null, permissionType, isDurable, isActive));
    public static async Task<ResponseBaseModel> RemoveAsync(this QueueBuilderModel queueBuilder, string queueKey)
        => await queueBuilder.client.Connect.InvokeAsync<ResponseBaseModel>("QueueRemove", queueKey);
}


public class QueueBuilderModel
{
    internal IClient client { get; private set; }
    public QueueBuilderModel(ref IClient client)
        => this.client = client;
}
