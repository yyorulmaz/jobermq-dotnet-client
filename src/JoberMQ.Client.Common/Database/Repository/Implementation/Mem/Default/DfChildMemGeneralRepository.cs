using JoberMQ.Client.Common.Database.Repository.Abstraction.Mem;
using System.Collections.Concurrent;

namespace JoberMQ.Common.Database.Repository.Implementation.Mem.Default
{
    internal class DfChildMemGeneralRepository<TKey, TValue> : IChildMemGeneralRepository<TKey, TValue>
    {
        #region Constructor
        public DfChildMemGeneralRepository(IMemRepository<TKey, TValue> masterData)
        {
            this.masterData = masterData;
            childData = new ConcurrentDictionary<TKey, TValue>();
        }
        #endregion

        #region Data
        private readonly IMemRepository<TKey, TValue> masterData;
        public IMemRepository<TKey, TValue> MasterData => masterData;


        private readonly ConcurrentDictionary<TKey, TValue> childData;
        public ConcurrentDictionary<TKey, TValue> ChildData => childData;
        #endregion

        #region Count
        public int Count => childData.Count;
        #endregion

        #region CRUD
        public TValue Get()
        {
            throw new NotImplementedException();
        }
        public TValue Get(TKey key)
        {
            childData.TryGetValue(key, out TValue value);
            return value;
        }
        public bool Add(TKey key, TValue value)
        {
            masterData.Add(key, value);
            var result = childData.TryAdd(key, value);
            if (result)
                ChangedAdded?.Invoke(value);
            return result;
        }
        public TValue Remove(TKey key)
        {
            masterData.Remove(key);
            childData.TryRemove(key, out TValue value);
            if (value != null)
                ChangedRemoved?.Invoke(value);
            return value;
        }
        #endregion

        #region Changed
        public event Action<TValue> ChangedAdded;
        public event Action<TValue> ChangedUpdated;
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}
