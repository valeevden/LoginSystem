using Microsoft.Extensions.Caching.Memory;
using System;

namespace LoginSystem.Services
{
    public class MemoryCacheSingleton
    {
        public MemoryCache Cache { get; set; }

        private static MemoryCacheSingleton _instance;

        private MemoryCacheSingleton()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
        }

        public static MemoryCacheSingleton GetCacheInstance()
        {
            if (_instance == null)
            {
                _instance = new MemoryCacheSingleton();
            }
            return _instance;
        }
    }
}