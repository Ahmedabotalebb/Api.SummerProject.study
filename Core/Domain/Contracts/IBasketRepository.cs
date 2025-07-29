using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.BasketModule;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket Basket , TimeSpan? timeToLive= null);
        public Task<bool> DeleteBasketAsync(string id);
        public Task<CustomerBasket> GetCustomerBasketAsync(string key);
    }
}
