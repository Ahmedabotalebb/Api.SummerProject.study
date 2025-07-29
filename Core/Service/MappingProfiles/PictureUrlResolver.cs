using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransfereObjects.ProductModuleDTO;

namespace Service.MappingProfiles
{
    internal class PictureUrlResolver: IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;
        public PictureUrlResolver(IConfiguration configuration)
        {
            this._configuration= configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))

                return string.Empty;
            else {
                //BaseUrl+PictureUrl
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return Url;

            }
        }
    }
}
