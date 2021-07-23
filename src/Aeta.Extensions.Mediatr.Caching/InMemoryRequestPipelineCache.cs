using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Aeta.Extensions.Mediatr.Caching
{
    public sealed class InMemoryRequestPipelineCache<TRequest> : IRequestPipelineCache<TRequest>, IDisposable
        where TRequest : IBaseRequest
    {
        private readonly IMemoryCache _cache;
        private readonly HashSet<string> _keys;
        private readonly string _requestType;

        public InMemoryRequestPipelineCache(IMemoryCache cache)
        {
            _cache = cache;
            _keys = new HashSet<string>();
            _requestType = typeof(TRequest).Name;
        }

        public void Dispose()
        {
            foreach (var key in _keys) _cache.Remove(key);
        }

        public async Task<TItem> GetItemAsync<TItem>(string key, Func<Task<TItem>> getItemOnMiss)
        {
            var cacheKey = _requestType + key;
            _keys.Add(cacheKey);
            var item = await _cache.GetOrCreateAsync(cacheKey, _ => getItemOnMiss());
            return item;
        }
    }
}