namespace JoberMQ.Common.Enums.Declare
{
    public enum DeclareConsumeTypeEnum
    {
        SpecialAdd = 1,
        SpecialRemove = 2,
        GroupAdd = 3,
        GroupRemove = 4,
        QueueAdd = 5,
        QueueRemove = 6,

        ErrorSpecialAdd = 7,
        ErrorSpecialRemove = 8,
        ErrorGroupAdd = 9,
        ErrorGroupRemove = 10,
        ErrorQueueAdd = 11,
        ErrorQueueRemove = 12,
    }
}
