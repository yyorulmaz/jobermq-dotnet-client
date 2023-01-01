namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClientOffline : IDisposable
    {
        public bool IsOfflineMode { get; }
    }
}
