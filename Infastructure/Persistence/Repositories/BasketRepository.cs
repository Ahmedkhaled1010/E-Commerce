﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase database=connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket =JsonSerializer.Serialize(basket);
          var IsCreatedOrUpdated= await database.StringSetAsync(basket.Id, JsonBasket,TimeToLive??TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
         return await  database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
                var Basket =await database.StringGetAsync(Key);
            if (Basket.IsNullOrEmpty)
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
            }
        }
    }
}
