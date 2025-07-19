using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DataSeeding(StoreDbcontext _DbContext): IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            var MigrationCheck = await _DbContext.Database.GetPendingMigrationsAsync();
            if (MigrationCheck.Any())
            {
                await _DbContext.Database.MigrateAsync();
            }
            try
            {

                if (!_DbContext.Set<ProductBrand>().Any()) 
                {
                    var productBrandsData = File.OpenRead(@"..\\Infrastracture\\Persistence\\Data\\DataSeedingData\\brands.json");

                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);

                    if (ProductBrands is not null && ProductBrands.Any())
                       await  _DbContext.productBrands.AddRangeAsync(ProductBrands);
                }
                if (!_DbContext.Set<ProductType>().Any())
                {
                    var productTypesData = File.OpenRead(@"..\\Infrastracture\\Persistence\\Data\\DataSeedingData\\types.json");

                    var ProductTypes =await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);

                    if (ProductTypes is not null && ProductTypes.Any())
                       await _DbContext.productTypes.AddRangeAsync(ProductTypes);
                }
                if (!_DbContext.Set<Product>().Any()) 
                {
                    var productData = File.OpenRead(@"..\\Infrastracture\\Persistence\\Data\\DataSeedingData\\products.json");

                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);

                    if (Products is not null && Products.Any())
                        await _DbContext.Products.AddRangeAsync(Products);
                }

            await _DbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                //todo
            }


        }
    }
}
