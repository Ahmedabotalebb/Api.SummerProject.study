using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Service.Specefications
{
    internal class ProductBrandAndTypeSpecification : BaseSpecification<Product,int>
    {
        public ProductBrandAndTypeSpecification(int? BrandId, int? TypeId) 
            :base(P=>(!TypeId.HasValue || P.TypeId==TypeId )&&
                (!BrandId.HasValue || P.BrandId==BrandId))
        {
            AddInclude(b=>b.productBrand);  
            AddInclude(b=>b.productType);  
        }

        public ProductBrandAndTypeSpecification(int id):base(B=>B.Id==id)
        {
            AddInclude(b => b.productBrand);
            AddInclude(b => b.productType);
        }


    }
}
