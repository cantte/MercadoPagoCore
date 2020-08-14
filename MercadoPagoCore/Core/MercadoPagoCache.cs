using System;
using MercadoPagoCore.Core.Cache;
using MercadoPagoCore.Exceptions;

namespace MercadoPagoCore.Core
{
    public static class MercadoPagoCache
    {
        private static readonly CacheItemDictionary<string, object> cache = new CacheItemDictionary<string, object>();
        public static void AddToCache(string key, MercadoPagoAPIResponse response)
        {
            try
            {
                cache.Add(key, response);
            }
            catch (Exception ex)
            {
                throw new MercadoPagoException("An error has occured in the cache structure (ADD): " + ex.Message);
            }
        }

        public static MercadoPagoAPIResponse GetFromCache(string key)
        {
            try
            {
                if (cache.TryGetValue(key, out object value))
                {
                    return (MercadoPagoAPIResponse)value;
                }
                else
                {
                    throw new MercadoPagoException("An error has occured in the cache structure (GET)");
                }
            }
            catch (Exception ex)
            {
                throw new MercadoPagoException("An error has occured in the cache structure (GET): " + ex.Message);
            }
        }

        public static void RemoveFromCache(string key)
        {
            try
            {
                cache.Remove(key);
            }
            catch (Exception ex)
            {
                throw new MercadoPagoException("An error has occured in the cache structure (REMOVE): " + ex.Message);
            }
        }
    }
}
