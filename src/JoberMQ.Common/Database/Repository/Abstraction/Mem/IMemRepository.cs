using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JoberMQ.Common.Database.Repository.Abstraction.Mem
{
    //IDb
    internal interface IMemRepository<TKey, TValue>
    {
        #region Data
        public ConcurrentDictionary<TKey, TValue> MasterData { get; }
        #endregion

        #region Count
        public int Count { get; }
        #endregion

        #region CRUD
        public TValue Get(TKey key);
        public TValue Get(Func<TValue, bool> filter);
        public List<TValue> GetAll(Func<TValue, bool> filter = null);
        public bool Add(TKey key, TValue value);
        public bool Update(TKey key, TValue value);
        public TValue Remove(TKey key);
        #endregion

        #region Changed
        public event Action<TValue> ChangedAdded;
        public event Action<TValue> ChangedUpdated;
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}
