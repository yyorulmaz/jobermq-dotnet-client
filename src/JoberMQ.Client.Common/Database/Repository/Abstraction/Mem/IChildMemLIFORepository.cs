using System.Collections.Concurrent;

namespace JoberMQ.Client.Common.Database.Repository.Abstraction.Mem
{
    //IDbChildLIFO
    internal interface IChildMemLIFORepository<TKey, TValue> : IChildMemRepository<TKey, TValue>
    {
        #region Data
        ConcurrentStack<TValue> ChildData { get; }
        #endregion
    }
}
