using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfereObjects.Authentication
{
    public class AddressDto
    {
        public string firstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Streat { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
