namespace JoberMQ.Client.Net.Abstract.Client
{
    public interface IClientOffline : IDisposable
    {
        public bool IsOfflineMode { get; }
    }
}
