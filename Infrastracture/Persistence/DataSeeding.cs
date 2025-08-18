using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DataSeeding(StoreDbcontext _DbContext,
        UserManager<ApplicationUser> _userManager,RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _identityDbContext): IDataSeeding
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

        public async Task IdentityDataSeedAsync()
        {
            if (_identityDbContext.Database.GetPendingMigrations().Any())
                _identityDbContext.Database.Migrate();


            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Ahmedabotaleb123555@gmail.com",
                        UserName = "Ahmed_abotaleb",
                        PhoneNumber = "01096093558",
                        DisplayName = "Ahmed AboTaleb"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Motasem123@gamil.com",
                        UserName = "motasem_123",
                        PhoneNumber = "01096093558",
                        DisplayName = "Motasem abotaleb"
                    };

                    await _userManager.CreateAsync(User01, "P@ss0rd");
                    await _userManager.CreateAsync(User02, "P@ss0rd");


                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");
                }
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

            }

        }
    }
}                                       
