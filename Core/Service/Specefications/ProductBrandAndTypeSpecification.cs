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
        public ProductBrandAndTypeSpecification():base(null)
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
