using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransfereObjects.BasketModuleDto;

namespace Shared.DataTransfereObjects.BasketDto
{
    public class BasketDto
    {
        public string Id { get; set; }  // GUID
        public IEnumerable<BasketItemDto> items { get; set; } = [];
    }
}
