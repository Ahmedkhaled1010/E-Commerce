﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase database =connection.GetDatabase();
        public async Task<string?> GetAsync(string CacheKey)
        {
            var CacheValue =await database.StringGetAsync(CacheKey);
            return CacheValue.IsNullOrEmpty ? null : CacheValue.ToString();
        }

        public async Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
               await database.StringSetAsync(CacheKey, CacheValue, TimeToLive);
        }
    }
}
