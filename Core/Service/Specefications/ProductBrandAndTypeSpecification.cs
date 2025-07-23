using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Service.Specefications
{
    internal class ProductBrandAndTypeSpecification : BaseSpecification<Product,int>
    {
        public ProductBrandAndTypeSpecification(int? BrandId, int? TypeId,ProductSortingOptions sortingOption) 
            :base(P=>(!TypeId.HasValue || P.TypeId==TypeId )&&
                (!BrandId.HasValue || P.BrandId==BrandId))
        {
            AddInclude(b=>b.productBrand);  
            AddInclude(b=>b.productType);




            switch (sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.Priceasc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    break;
            }


        }

        public ProductBrandAndTypeSpecification(int id):base(B=>B.Id==id)
        {
            AddInclude(b => b.productBrand);
            AddInclude(b => b.productType);
        }



    }
}
