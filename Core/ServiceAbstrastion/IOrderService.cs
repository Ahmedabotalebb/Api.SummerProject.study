using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransfereObjects.OrderDtos;

namespace ServiceAbstrastion
{
    public interface IOrderService
    {
        public Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto);
    }
}
