namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClientInfo
    {
        string ClientKey { get; set; }
        string ClientGroupKey { get; set; }
        bool IsClientActive { get; }
        bool IsOfflineClient { get; }
    }
}
