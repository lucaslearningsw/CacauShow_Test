using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacauShow.API.Domain.Interfaces;
using CacauShow.API.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CacauShow.API.Data.Repository
{
    public class CachingRepository : ICachingRepository
    {
        private readonly IDistributedCache _distributedCache;

        public CachingRepository(IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
        }
        public async Task<T> GetValue<T>(Guid id)
        {
            var key = id.ToString().ToLower();

            var result = await _distributedCache.GetStringAsync(key);

            if (result == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<IEnumerable<T>> GetCollection<T>(string collectionKey)
        {
            var result = await _distributedCache.GetStringAsync(collectionKey);

            if (result == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<IEnumerable<T>>(result);
        }

        public async  Task SetValue<T>(Guid id, T obj)
        {
            var key = id.ToString().ToLower();
            var newValue = JsonConvert.SerializeObject(obj);
            await _distributedCache.SetStringAsync(key, newValue);;
        }

        public async Task SetCollection<T>(IEnumerable<T> collection, string collectionKey)
        {
            var users = JsonConvert.SerializeObject(collection);

           await _distributedCache.SetStringAsync(collectionKey,users);
        }

        public async Task DeleteValue<T>(Guid id)
        {
            var key = id.ToString().ToLower();
            _distributedCache.Remove(key);
        }
    }
}
