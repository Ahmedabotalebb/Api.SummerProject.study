using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using ServiceAbstrastion;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper ,IBasketRepository _BasketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        public IProductService productService => _lazyProductService.Value;

        private readonly Lazy<IBasketService> _LazyBasektService = new Lazy<IBasketService>(() => new BasketService(_BasketRepository, _mapper));
        public IBasketService BasketService => _LazyBasektService.Value;
    }
}
