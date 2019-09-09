using System;
using System.Collections.Generic;
using System.Threading;

namespace Mimax.Cache
{
    public class SynchronizedCache<TKey, TValue>
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private Dictionary<TKey, TValue> _items = new Dictionary<TKey, TValue>();

        public int Count => _items.Count;

        public TValue Read(TKey key)
        {
            _lock.EnterReadLock();
            try
            {
                if (_items.ContainsKey(key))
                    return _items[key];
                return default;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Add(TKey key, TValue value)
        {
            _lock.EnterWriteLock();
            try
            {
                _items[key] = value;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public IEnumerable<TResult> Select<TResult>(Func<(TKey, TValue), TResult> select)
        {
            _lock.EnterReadLock();
            try
            {
                var results = new List<TResult>();
                foreach (var item in _items)
                {
                    var result = default(TResult);
                    try
                    {
                        result = select((item.Key, item.Value));
                    }
                    catch
                    {
                        continue;
                    }
                    results.Add(result);
                }
                return results;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Delete(TKey key)
        {
            _lock.EnterWriteLock();
            try
            {
                if (_items.ContainsKey(key))
                    _items.Remove(key);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        ~SynchronizedCache()
        {
            _lock?.Dispose();
        }
    }
}