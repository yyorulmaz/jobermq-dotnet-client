using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;

public static class QueueExtension
{
    public static QueueAddBuilderModel Add(this QueueBuilderModel builder, string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isActive)
        => new QueueAddBuilderModel(builder.client, queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isActive);

    public static QueueGetBuilderModel Get(this QueueBuilderModel builder, string queueKey)
        => new QueueGetBuilderModel(builder.client, queueKey);

    public static QueueGetAllBuilderModel GetAll(this QueueBuilderModel builder)
        => new QueueGetAllBuilderModel(builder.client);

    public static QueueEditBuilderModel Edit(this QueueBuilderModel builder, string queueKey, string[] tags, PermissionTypeEnum permissionType = PermissionTypeEnum.All, bool isDurable = true, bool isActive = true)
        => new QueueEditBuilderModel(builder.client, queueKey, tags, permissionType, isDurable, isActive);

    public static QueueRemoveBuilderModel Remove(this QueueBuilderModel builder, string distributorKey)
        => new QueueRemoveBuilderModel(builder.client, distributorKey);
}