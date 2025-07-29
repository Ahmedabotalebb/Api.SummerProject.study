using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.BasketDto;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var Customerbasket= _mapper.Map<BasketDto,CustomerBasket>(basket);
            var IscreatedOrUpdated=await _basketRepository.CreateOrUpdateBasketAsync(Customerbasket);
            if (IscreatedOrUpdated is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can't Create or Update Basekt Now");
        }

        public async Task<bool> DeleteBasketAsync(string key)=>await _basketRepository.DeleteBasketAsync(key);

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket=await _basketRepository.GetCustomerBasketAsync(key);
            if (basket is not null)
                return _mapper.Map<CustomerBasket, BasketDto>(basket);
            else
                throw new BasketNotFoundException(key);
        }

    }
}
