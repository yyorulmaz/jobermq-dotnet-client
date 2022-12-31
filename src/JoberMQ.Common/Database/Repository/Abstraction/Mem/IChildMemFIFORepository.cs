using System.Collections.Concurrent;

namespace JoberMQ.Common.Database.Repository.Abstraction.Mem
{
    //IDbChildFIFO
    internal interface IChildMemFIFORepository<TKey, TValue> : IChildMemRepository<TKey, TValue>
    {
        #region Data
        ConcurrentQueue<TValue> ChildData { get; }
        #endregion
    }
}
