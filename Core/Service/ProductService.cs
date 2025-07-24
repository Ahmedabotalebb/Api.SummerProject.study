using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Service.Specefications;
using ServiceAbstrastion;
using Shared;
using Shared.DataTransfereObjects;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    { 
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Brands= await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
        }

        public async Task<PagenationResult<ProductDto>> GetAllProductsAsync(ProductQuery productQuery)
        {
            var Specifications = new ProductBrandAndTypeSpecification(productQuery);
            var Products =await  _unitOfWork.GetRepository<Product,int>().GetAllAsync(Specifications);
            var AllproductDto = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(Products);
            var productCount = AllproductDto.Count();
            return new PagenationResult<ProductDto>(productQuery.PageIndex, 0, productCount, AllproductDto);
        }

        public async Task<IEnumerable<TypesDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable<TypesDto>>(Types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductBrandAndTypeSpecification(id);
            var Product=await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(Specifications);
            return _mapper.Map<Product,ProductDto>(Product);
        }
    }
}
