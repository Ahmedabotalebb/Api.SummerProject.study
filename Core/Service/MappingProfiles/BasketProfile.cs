using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.BasketModule;
using Shared.DataTransfereObjects.BasketDto;
using Shared.DataTransfereObjects.BasketModuleDto;

namespace Service.MappingProfiles
{
    public class BasketProfile :Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
        }
    }
}
