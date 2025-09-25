using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.OrderModule
{

    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }

        public Order(string userEmail, DeliveryMethod deliveryMethod, ShipingAddress shipingAddress, ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            DeliveryMethod = deliveryMethod;
            ShipingAddress = shipingAddress;
            Items = items;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ShipingAddress ShipingAddress { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }



        OrderStatus Status { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;

        public int DeliveryMethodId { get; set; } //fk



    }
}
