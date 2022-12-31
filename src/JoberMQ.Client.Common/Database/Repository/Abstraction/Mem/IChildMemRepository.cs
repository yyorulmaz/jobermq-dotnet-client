using System;

namespace JoberMQ.Client.Common.Database.Repository.Abstraction.Mem
{
    //IDbChild
    internal interface IChildMemRepository<TKey, TValue>
    {
        #region Data
        public IMemRepository<TKey, TValue> MasterData { get; }
        #endregion

        #region Count
        public int Count { get; }
        #endregion

        #region CRUD
        public TValue Get();
        public TValue Get(TKey key);
        public bool Add(TKey key, TValue value);
        public TValue Remove(TKey key);
        #endregion

        #region Changed
        public event Action<TValue> ChangedAdded;
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}
