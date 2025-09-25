using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.OrderDtos;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager):ApiBaseController
    {
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto Order)
        {
            var order = await _serviceManager.OrderService.CreateOrderAsync(Order, GetEmailFromToken());
            return Ok(order);
        }
    }
}
