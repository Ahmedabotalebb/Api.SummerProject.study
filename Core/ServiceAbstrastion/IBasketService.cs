using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransfereObjects.BasketDto;

namespace ServiceAbstrastion
{
    public interface IBasketService
    {
        public Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);
        public Task<bool> DeleteBasketAsync(string key);
        public Task<BasketDto> GetBasketAsync(string key);
    }
}
