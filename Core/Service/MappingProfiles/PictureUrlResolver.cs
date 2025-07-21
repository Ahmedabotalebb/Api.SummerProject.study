using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared.DataTransfereObjects;

namespace Service.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {
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
