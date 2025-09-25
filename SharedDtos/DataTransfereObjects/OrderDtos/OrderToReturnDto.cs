using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransfereObjects.Authentication;

namespace Shared.DataTransfereObjects.OrderDtos
{
    public class OrderToReturnDto
    {
        public string UserEmail { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public AddressDto ShipingAddress { get; set; } = default!;
        public ICollection<OrderItemDto> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

    }
}
