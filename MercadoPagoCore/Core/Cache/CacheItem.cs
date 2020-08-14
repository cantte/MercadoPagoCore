using System;

namespace MercadoPagoCore.Core.Cache
{
    public class CacheItem<T>
    {
        public CacheItem()
        {
            TimeStamp = DateTime.Now;
            TimeToLive = TimeSpan.FromMinutes(20);
        }

        public CacheItem(T value) : this()
        {
            Value = value;
        }

        public CacheItem(T value, TimeSpan timeToLive) : this(value)
        {
            TimeToLive = timeToLive;
        }

        public CacheItem(T value, DateTime expires) : this(value)
        {
            Expires = expires;
        }

        public CacheItem(T value, DateTime timeStamp, TimeSpan timeToLive) : this(value, timeToLive)
        {
            TimeStamp = timeStamp;
        }

        public CacheItem(T value, DateTime timeStamp, DateTime expires) : this(value)
        {
            TimeStamp = timeStamp;
            Expires = expires;
        }

        public T Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public bool NeverExpire { get; set; }
        public DateTime Expires
        {
            get
            {
                return TimeStamp + TimeToLive;
            }
            set
            {
                TimeToLive = value - TimeStamp;
            }
        }

        public bool HasExpired
        {
            get
            {
                return (!NeverExpire && DateTime.Now > Expires);
            }
        }
    }
}
