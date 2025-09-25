using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransfereObjects.Authentication;

namespace Shared.DataTransfereObjects.OrderDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
        public AddressDto OrderAddress { get; set; } = default!;
        public int DelivertyMethodId { get; set; }
        public string Email { get; set; } = default!;

    }
}
