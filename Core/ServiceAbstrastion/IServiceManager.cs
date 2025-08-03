using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstrastion
{
    public interface IServiceManager
    {
        public IProductService productService { get; }
        public IBasketService BasketService { get; }
        public IAuthenticationService authentication { get; }
    }
}
