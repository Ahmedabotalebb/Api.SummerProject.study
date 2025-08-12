using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstrastion;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper ,IBasketRepository _BasketRepository ,UserManager<ApplicationUser> user,IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _LazyBasektService = new Lazy<IBasketService>(() => new BasketService(_BasketRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _LazyauthenticationService =new Lazy<IAuthenticationService>(()=>new AuthenticationService(user, configuration));

        public IProductService productService => _lazyProductService.Value;

        public IBasketService BasketService => _LazyBasektService.Value;

        public IAuthenticationService authentication => _LazyauthenticationService.Value;
    }
}
