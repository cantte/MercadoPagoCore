using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MercadoPagoCore.Core.Cache
{
    public class CacheItemDictionary<K, T> : IDictionary<K, T>, IDisposable
    {
        public event EventHandler<CacheItemRemovedEventArgs<K, T>> ItemExpired;
        private readonly object lockObject = new object();

        public CacheItemDictionary()
        {
            CacheItems = new Dictionary<K, CacheItem<T>>();
        }

        protected Timer Timer { get; set; }
        private TimeSpan timeSpan = TimeSpan.FromSeconds(15);

        public TimeSpan AutoClearExpiredItemsFrequency
        {
            get { return timeSpan; }
            set
            {
                timeSpan = value;
                Timer.Change(value, value);
            }
        }

        public TimeSpan DefaultTimeToLive { get; set; }
        public Dictionary<K, CacheItem<T>> CacheItems { get; }

        public void Add(K key, T value)
        {
            CacheItems.Add(key, new CacheItem<T>(value, DefaultTimeToLive));
        }

        public void Add(K key, T value, TimeSpan timeToLive)
        {
            CacheItems.Add(key, new CacheItem<T>(value, timeToLive));
        }

        public void Add(K key, T value, DateTime expires)
        {
            CacheItems.Add(key, new CacheItem<T>(value, expires));
        }

        public void Add(KeyValuePair<K, T> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(KeyValuePair<K, CacheItem<T>> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(K key, CacheItem<T> value)
        {
            CacheItems.Add(key, value);
        }

        public bool ContainsKey(K key)
        {
            lock (lockObject)
            {
                if (CacheItems.ContainsKey(key))
                {
                    if (CacheItems[key].HasExpired)
                    {
                        ItemExpired?.Invoke(this, new CacheItemRemovedEventArgs<K, T>
                        {
                            Key = key,
                            Value = CacheItems[key].Value
                        });
                        CacheItems.Remove(key);
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }

        public ICollection<K> Keys
        {
            get
            {
                lock (lockObject)
                {
                    ClearExpiredItems();
                    return CacheItems.Keys;
                }
            }
        }

        public bool Remove(K key)
        {
            lock (lockObject)
            {
                if (ContainsKey(key))
                {
                    CacheItems.Remove(key);
                    return true;
                }
                return false;
            }
        }

        public bool TryGetValue(K key, out T value)
        {
            lock (lockObject)
            {
                if (ContainsKey(key))
                {
                    value = CacheItems[key].Value;
                    return true;
                }
                value = default;
                return false;
            }
        }

        public ICollection<T> Values
        {
            get
            {
                return this.Cast<T>().ToList();
            }
        }

        public T this[K key]
        {
            get
            {
                lock (lockObject)
                {
                    if (ContainsKey(key))
                        return CacheItems[key].Value;
                    return default;
                }
            }
            set
            {
                CacheItems[key] = new CacheItem<T>(value, DefaultTimeToLive);
            }
        }

        public T this[K key, TimeSpan timeToLive]
        {
            set
            {
                CacheItems[key] = new CacheItem<T>(value, timeToLive);
            }
        }

        public T this[K key, DateTime expires]
        {
            set
            {
                CacheItems[key] = new CacheItem<T>(value, expires);
            }
        }

        public void Clear()
        {
            CacheItems.Clear();
        }

        bool ICollection<KeyValuePair<K, T>>.Contains(KeyValuePair<K, T> item)
        {
            lock (lockObject)
            {
                return ContainsKey(item.Key) &&
                       (object)CacheItems[item.Key].Value
                       == (object)item.Value;
            }
        }

        void ICollection<KeyValuePair<K, T>>.CopyTo(KeyValuePair<K, T>[] array, int arrayIndex)
        {
            // if you need it, implement it
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                lock (lockObject)
                {
                    ClearExpiredItems();
                    return CacheItems.Count;
                }
            }
        }

        bool ICollection<KeyValuePair<K, T>>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<K, T>>.Remove(KeyValuePair<K, T> item)
        {
            lock (lockObject)
            {
                if (ContainsKey(item.Key))
                {
                    CacheItems.Remove(item.Key);
                    return true;
                }
                return false;
            }
        }

        public void ClearExpiredItems()
        {
            lock (lockObject)
            {
                List<KeyValuePair<K, CacheItem<T>>> removeList
                    = CacheItems.Where(kvp => kvp.Value.HasExpired).ToList();

                removeList.ForEach(kvp =>
                {
                    ItemExpired?.Invoke(this, new CacheItemRemovedEventArgs<K, T>
                    {
                        Key = kvp.Key,
                        Value = kvp.Value.Value
                    });

                    CacheItems.Remove(kvp.Key);
                });
            }
        }

        public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        {
            lock (lockObject)
            {
                ClearExpiredItems();
                Dictionary<K, T> ret = CacheItems.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Value);
                return ret.GetEnumerator();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Update(K key, DateTime resetTimestamp)
        {
            lock (lockObject)
            {
                if (ContainsKey(key)) CacheItems[key].TimeStamp = resetTimestamp;
                ClearExpiredItems();
            }
        }

        public T GetWithUpdateOrCreate(K key)
        {
            lock (lockObject)
            {
                T retval;
                if (!ContainsKey(key))
                {
                    this[key] = retval = (T)Activator.CreateInstance(typeof(T));
                }
                else retval = this[key];
                CacheItems[key].TimeStamp = DateTime.Now;
                return retval;
            }
        }

        public void Dispose()
        {
            Timer.Dispose();
        }
    }
}
