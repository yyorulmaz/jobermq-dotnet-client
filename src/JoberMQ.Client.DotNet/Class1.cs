using System;
using System.Collections.Concurrent;

namespace JoberMQ.Client.DotNet
{
    public interface IDataStore<Key, Value> : IDisposable
    {
        bool Add(Key key, Value value);
        bool Get(Key key, out Value outValue);
    }
    public class DataStore<Key, Value> : IDataStore<Key, Value>
    {
        ConcurrentDictionary<Key, Value> data = new ConcurrentDictionary<Key, Value>();

        public bool Add(Key key, Value value) => data.TryAdd(key, value);
        public bool Get(Key key, out Value outValue) => data.TryGetValue(key, out outValue);

        public void test()
        {

        }

        #region Dispose
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DataStore()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }







}


/*
 
 
ddd.TryAdd("ali", "hayde");
ddd.TryAdd(15, 9);

ddd.TryGetValue("ali", out var deneme);
Console.WriteLine(deneme);
ddd.TryGetValue(15, out var deneme2);
Console.WriteLine(deneme2);

 */