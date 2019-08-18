using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Shared;
using Shared.Services;
using System;

namespace SearchEngine.Services
{    
    public class InMemoryCachingService : ICachingService
    {
        private readonly IMemoryCache _cache;

        private readonly MemoryCacheSettings _cacheSettings;

        public InMemoryCachingService(IMemoryCache memoryCache, IOptions<MemoryCacheSettings> cacheSettings)
        {
            _cache = memoryCache;
            _cacheSettings = cacheSettings.Value;
        }
                
        public string Get(string key)
        {
            Guard.ListNotNullOrEmpty(key, nameof(key));

            return _cache.Get<string>(key);
        }
                
        public void Set(string key, string value)
        {
            Guard.ListNotNullOrEmpty(key, nameof(key));
            Guard.ListNotNullOrEmpty(value, nameof(value));

            _cache.Set(key, value,
                new DateTimeOffset(DateTime.Now.AddHours(_cacheSettings.ExpirationTimeHours)));
        }
    }
}
