using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.OrderDtos;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager):ApiBaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto Order)
        {
            var order = await _serviceManager.OrderService.CreateOrderAsync(Order, GetEmailFromToken());
            return Ok(order);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Orders =await _serviceManager.OrderService.GetAllOdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMthodDto>>> GetDeliveyMethods()
        {
            var Methods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(Methods);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid Id)
        {
            var order = await _serviceManager.OrderService.GetOderByIdAsync(Id);
            return Ok(order);
           }

    }
}
