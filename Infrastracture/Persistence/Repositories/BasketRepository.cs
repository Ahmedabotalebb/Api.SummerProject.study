using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _Database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket Basket, TimeSpan? timeToLive = null)
        {
            var JsonBasket= JsonSerializer.Serialize(Basket);
            var IsCreatedOrUpdated = await _Database.StringSetAsync(Basket.Id,JsonBasket,timeToLive ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetCustomerBasketAsync(Basket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string id)=> await _Database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetCustomerBasketAsync(string key)
        {
            var basket = await _Database.StringGetAsync(key);
            if (basket.IsNullOrEmpty)
                return null;
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(basket!);
            }
        }
    }
}
