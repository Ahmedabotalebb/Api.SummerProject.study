using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Service.Specefications
{
    internal class ProductCountSpecification:BaseSpecification<Product,int>
    {
        public ProductCountSpecification(ProductQuery productQuery)
                  : base(P => (!productQuery.TypeId.HasValue || P.TypeId == productQuery.TypeId) &&
                      (!productQuery.BrandId.HasValue || P.BrandId == productQuery.BrandId) &&
                      (string.IsNullOrEmpty(productQuery.SearchValue) || P.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))
        {
            
        }
    }
}
