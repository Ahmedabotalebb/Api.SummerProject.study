using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransfereObjects.OrderDtos;
using Shared.DataTransfereObjects.ProductModuleDTO;

namespace Service.MappingProfiles
{
    internal class OrderItemPictureUrlResolver:IValueResolver<OrderItem,OrderItemDto,string>
    {
        private readonly IConfiguration _configuration;
        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ProductItemOredered.PictureUrl))

                return string.Empty;
            else
            {
                //BaseUrl+PictureUrl
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.ProductItemOredered.PictureUrl}";
                return Url;

            }
        }
    }
}
