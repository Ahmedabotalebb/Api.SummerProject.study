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
        public ProductBrandAndTypeSpecification(ProductQuery productQuery)
            : base(P => (!productQuery.TypeId.HasValue || P.TypeId == productQuery.TypeId) &&
                (!productQuery.BrandId.HasValue || P.BrandId == productQuery.BrandId) &&
                (string.IsNullOrEmpty(productQuery.SearchValue) || P.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))
        {
            AddInclude(b=>b.productBrand);  
            AddInclude(b=>b.productType);




            switch (productQuery.SortingOption)
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

            ApplyPagenation(productQuery.PageSize, productQuery.PageIndex);



           
        }

        public ProductBrandAndTypeSpecification(int id):base(B=>B.Id==id)
        {
            AddInclude(b => b.productBrand);
            AddInclude(b => b.productType);
        }



    }
}
