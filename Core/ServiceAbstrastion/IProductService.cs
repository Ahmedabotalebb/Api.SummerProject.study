using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransfereObjects;

namespace ServiceAbstrastion
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypesDto>> GetAllTypesAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
    }
}
