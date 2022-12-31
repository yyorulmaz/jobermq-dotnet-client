using System.Collections.Concurrent;

namespace JoberMQ.Common.Database.Repository.Abstraction.Mem
{
    //IDbChildPriority
    internal interface IChildMemGeneralRepository<TKey, TValue> : IChildMemRepository<TKey, TValue>
    {
        #region Data
        ConcurrentDictionary<TKey, TValue> ChildData { get; }
        #endregion
    }
}
