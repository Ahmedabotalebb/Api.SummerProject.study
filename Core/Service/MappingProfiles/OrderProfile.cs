using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.OrderModule;
using Shared.DataTransfereObjects.Authentication;
using Shared.DataTransfereObjects.OrderDtos;

namespace Service.MappingProfiles
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, ShipingAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>().ForMember(D => D.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>().ForMember(D => D.Name, o => o.MapFrom(s => s.ProductItemOredered.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

        }
    }
}
