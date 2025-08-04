using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.BasketDto;

namespace Presentation.Controllers
{

    public class BasketController(IServiceManager _serviceManager) :ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDto);
            return Ok(Basket);
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }

        [HttpDelete("{Key}")]
        public async Task<bool> DeleteBasket(string key)
        {
            return await _serviceManager.BasketService.DeleteBasketAsync(key);
        }
    }
}
