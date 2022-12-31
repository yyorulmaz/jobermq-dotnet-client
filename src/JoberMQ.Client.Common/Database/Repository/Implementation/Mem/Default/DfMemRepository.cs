using JoberMQ.Client.Common.Database.Repository.Abstraction.Mem;
using System.Collections.Concurrent;

namespace JoberMQ.Common.Database.Repository.Implementation.Mem.Default
{
    internal class DfMemRepository<TKey, TValue> : IMemRepository<TKey, TValue>
    {
        #region Constructor
        public DfMemRepository(ConcurrentDictionary<TKey, TValue> masterData)
        {
            this.masterData = masterData;
        }
        #endregion

        #region Data
        private readonly ConcurrentDictionary<TKey, TValue> masterData;
        public ConcurrentDictionary<TKey, TValue> MasterData => masterData;
        #endregion

        #region Count
        public int Count => masterData.Count;
        #endregion

        #region CRUD
        public TValue Get(TKey key)
        {
            masterData.TryGetValue(key, out TValue value);
            return value;
        }
        public TValue Get(Func<TValue, bool> filter)
        {
            return masterData.Values.FirstOrDefault(filter);
        }
        public List<TValue> GetAll(Func<TValue, bool> filter = null)
        {
            if (filter == null)
                return masterData.Values.ToList();
            else
                return masterData.Values.Where(filter).ToList();
        }
        public bool Add(TKey key, TValue value)
        {
            var result = masterData.TryAdd(key, value);
            if (result)
                ChangedAdded?.Invoke(value);
            return result;
        }
        public bool Update(TKey key, TValue value)
        {
            var result = masterData.TryUpdate(key, value, value);
            if (result)
                ChangedUpdated?.Invoke(value);
            return result;
        }
        public TValue Remove(TKey key)
        {
            masterData.TryRemove(key, out TValue value);
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
