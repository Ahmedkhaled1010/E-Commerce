using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using ServicesAbstraction;

namespace ServicesImplemetation
{
    public class CacheServices(ICacheRepository cache) : ICacheServices
    {
        public async Task<string> GetAsync(string key)
        {
            return await  cache.GetAsync(key);
        }

        public async Task SetAsync(string cachekey, object cachevalue, TimeSpan timeSpan)
        {
            var value =JsonSerializer.Serialize(cachevalue);
         await   cache.SetAsync(cachekey, value, timeSpan);

        }
    }
}
