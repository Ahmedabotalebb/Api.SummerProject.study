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
         Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto,string Email);

        Task<IEnumerable<DeliveryMthodDto>> GetDeliveryMethodsAsync();

        Task<IEnumerable<OrderToReturnDto>> GetAllOdersAsync(string email);

        Task<OrderToReturnDto> GetOderByIdAsync(Guid id);


    }
}
