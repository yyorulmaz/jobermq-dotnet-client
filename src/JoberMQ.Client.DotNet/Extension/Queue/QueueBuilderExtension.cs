using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Queue;

public static class QueueBuilderExtension
{
    public static QueueBuilderModel Queue(this IClient client)
        => new QueueBuilderModel(client);
}

public class QueueBuilderModel
{
    internal IClient client { get; set; }
    internal QueueBuilderModel(IClient client)
        => this.client = client;
}

public class QueueAddBuilderModel
{
    internal IClient client { get; set; }
    internal string queueKey { get; set; }
    internal string[] tags { get; set; }
    internal QueueMatchTypeEnum queueMatchType { get; set; }
    internal QueueOrderOfSendingTypeEnum queueOrderOfSendingType { get; set; }
    internal PermissionTypeEnum permissionType { get; set; }
    internal bool isDurable { get; set; }
    internal bool isActive { get; set; }
    internal QueueAddBuilderModel(
        IClient client,
        string queueKey,
        string[] tags,
        QueueMatchTypeEnum queueMatchType,
        QueueOrderOfSendingTypeEnum queueOrderOfSendingType,
        PermissionTypeEnum permissionType,
        bool isDurable,
        bool isActive)
    {
        this.client = client;
        this.queueKey = queueKey;
        this.tags = tags;
        this.queueMatchType = queueMatchType;
        this.queueOrderOfSendingType = queueOrderOfSendingType;
        this.permissionType = permissionType;
        this.isDurable = isDurable;
        this.isActive = isActive;
    }

    internal QueueModel GetQueueModel()
        => new QueueModel(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isActive);
}

public class QueueGetBuilderModel
{
    internal IClient client { get; set; }
    internal string queueKey { get; set; }
    internal QueueGetBuilderModel(IClient client, string queueKey)
    {
        this.client = client;
        this.queueKey = queueKey;
    }
}

public class QueueGetAllBuilderModel
{
    internal IClient client { get; set; }
    internal QueueGetAllBuilderModel(IClient client)
    {
        this.client = client;
    }
}

public class QueueEditBuilderModel
{
    internal IClient client { get; set; }
    internal string queueKey { get; set; }
    public string[] tags { get; set; }
    internal PermissionTypeEnum permissionType { get; set; }
    internal bool isDurable { get; set; }
    internal bool isActive { get; set; }
    internal QueueEditBuilderModel(
        IClient client,
        string queueKey,
        string[] tags,
        PermissionTypeEnum permissionType,
        bool isDurable,
        bool isActive)
    {
        this.client = client;
        this.queueKey = queueKey;
        this.tags = tags;
        this.permissionType = permissionType;
        this.isDurable = isDurable;
        this.isActive = isActive;
    }

    internal QueueModel GetQueueModel()
        => new QueueModel(queueKey, tags, null, null, permissionType, isDurable, isActive);
}

public class QueueRemoveBuilderModel
{
    internal IClient client { get; set; }
    internal string queueKey { get; set; }
    internal QueueRemoveBuilderModel(IClient client, string queueKey)
    {
        this.client = client;
        this.queueKey = queueKey;
    }
}
