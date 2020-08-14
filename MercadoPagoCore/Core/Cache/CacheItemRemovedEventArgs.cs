using System;

namespace MercadoPagoCore.Core.Cache
{
    public class CacheItemRemovedEventArgs<K, T> : EventArgs
    {
        public K Key { get; set; }
        public T Value { get; set; }
    }
}
