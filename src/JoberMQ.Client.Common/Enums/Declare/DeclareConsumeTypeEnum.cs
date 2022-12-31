namespace JoberMQ.Client.Common.Enums.Declare
{
    internal enum DeclareConsumeTypeEnum
    {
        DeclareConsumeSpecialAdd = 1,
        DeclareConsumeSpecialRemove = 2,
        DeclareConsumeGroupAdd = 3,
        DeclareConsumeGroupRemove = 4,
        DeclareConsumeQueueAdd = 5,
        DeclareConsumeQueueRemove = 6,

        DeclareConsumeErrorSpecialAdd = 7,
        DeclareConsumeErrorSpecialRemove = 8,
        DeclareConsumeErrorGroupAdd = 9,
        DeclareConsumeErrorGroupRemove = 10,
        DeclareConsumeErrorQueueAdd = 11,
        DeclareConsumeErrorQueueRemove = 12,
    }
}
