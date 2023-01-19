namespace JoberMQ.Client.Net.Enums.Status
{
    public enum StatusTypeMessageEnum
    {
        None = 1,
        SendClient = 2,
        Started = 3,
        Continues = 4,
        Temp = 5,
        Undeliverable = 6,
        Completed = 7,
        Cancel = 8
    }
}
