using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Specefications;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.Authentication;
using Shared.DataTransfereObjects.OrderDtos;

namespace Service
{
    public class OrderService(IMapper mapper , IBasketRepository basketRepository,IUnitOfWork unitOfWork) :IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto,string Email)
        {
            var OrderAddress = mapper.Map<AddressDto, ShipingAddress>(orderDto.OrderAddress);

            var Basket= await basketRepository.GetCustomerBasketAsync(orderDto.BasketId)??
                throw new BasketNotFoundException(orderDto.BasketId);

            List<OrderItem> orderItems = [];
            var productRepo = unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.items)
            {
                var product = await productRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFountException(item.Id);
                OrderItem orderItem = CreateOrderItem(item, product);
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DelivertyMethodId)??
                throw new DeliveryMethodNotFoundException(orderDto.DelivertyMethodId);

            var SubTotal = orderItems.Sum(I => I.Quantity * I.Price);


            var Order = new Order(orderDto.Email, deliveryMethod, OrderAddress, orderItems, SubTotal);
            await  unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await unitOfWork.SaveChangesAsync();
            return  mapper.Map<Order,OrderToReturnDto>(Order);
        }
         
        private static OrderItem CreateOrderItem(Domain.Models.BasketModule.BasketItem item, Product product)
        {
            return new OrderItem()
            {
                ProductItemOredered = new ProductItemOredered() { Id = product.Id, Name = product.Name, PictureUrl = product.PictureUrl },
                Price = product.Price,
                Quantity = item.Quantity
            };
        }



        public async Task<IEnumerable<DeliveryMthodDto>> GetDeliveryMethodsAsync()
        {
            var methods =  await unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMthodDto>>(methods);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOdersAsync(string email)
        {
            var spec = new OrderSpecifications(email);
            var Orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOderByIdAsync(Guid id)
        {
            var spec=new OrderSpecifications(id);
            var order= await unitOfWork.GetRepository<Order,Guid>().GetByIdAsync(spec);
            return mapper.Map<Order, OrderToReturnDto>(order);

        }
    }
}
