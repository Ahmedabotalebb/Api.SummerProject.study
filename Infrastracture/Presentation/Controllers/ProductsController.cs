using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstrastion;
using Shared;
using Shared.DataTransfereObjects.ProductModuleDTO;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager):ApiBaseController
    {
        //GetAllProducts
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<PagenationResult<ProductDto>>> GetAllProductsAsync([FromQuery]ProductQuery productQuery)
        {
            var Products = await _serviceManager.productService.GetAllProductsAsync(productQuery);
            return Ok(Products);
        }


        //Get Product By Id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductAsync(int id) //Started From 24
        {
            var Product = await _serviceManager.productService.GetProductByIdAsync(id);
            return Ok(Product);
        }


        //GetAll Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypesDto>>> GetAllTypesAsync()
        {
            var Types = await _serviceManager.productService.GetAllTypesAsync();
            return Ok(Types);
        }


        //Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands=await _serviceManager.productService.GetAllBrandsAsync();
            return Ok(brands);
        }

    }
}
