using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstrastion;
using Shared.DataTransfereObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager):ControllerBase
    {
        //GetAllProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync(int? BrandId , int? TypeId)
        {
            var Products = await _serviceManager.productService.GetAllProductsAsync(BrandId, TypeId);
            return Ok(Products);
        }

        //Get Product By Id
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
