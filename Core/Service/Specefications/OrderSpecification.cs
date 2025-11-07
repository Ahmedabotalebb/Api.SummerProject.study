using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;

namespace Service.Specefications
{
    class OrderSpecifications : BaseSpecification<Order,Guid>
    {
        public OrderSpecifications(string Email):base(o=>o.UserEmail==Email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(O=>O.Items);
            
        }
        public OrderSpecifications(Guid id):base(o=>o.Id==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(O=>O.Items);
            
        }

    }
}
