using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext storeDb,UserManager<ApplicationUser> 
        userManager,RoleManager<IdentityRole> roleManager,
        StoreIdentityDbContext storeIdentity) : IDataSeeding
    {
        public  async Task DataSeedAsync()
        {
            try
            {
                var Pending = await storeDb.Database.GetPendingMigrationsAsync();

                if (Pending.Any())
                {
                   await storeDb.Database.MigrateAsync();
                }
                if (!storeDb.ProductBrands.Any())
                {
                    var ProductBrand = File.OpenRead(@"..\Infastructure\Persistence\Data\DataSeed\brands.json");
                    var brands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrand);
                    if (brands is not null && brands.Any())
                    {
                       await storeDb.ProductBrands.AddRangeAsync(brands);
                        await storeDb.SaveChangesAsync();

                    }




                }
                if (!storeDb.ProductTypes.Any())
                {
                    var ProductType = File.OpenRead(@"..\Infastructure\Persistence\Data\DataSeed\types.json");
                    var Types = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductType);
                    if (Types is not null && Types.Any())
                    {
                      await  storeDb.ProductTypes.AddRangeAsync(Types);
                        await storeDb.SaveChangesAsync();

                    }
                }
                if (!storeDb.Products.Any())
                {
                    var ProductsData = File.OpenRead(@"..\Infastructure\Persistence\Data\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (products is not null && products.Any())
                    {
                        await storeDb.Products.AddRangeAsync(products);
                        await storeDb.SaveChangesAsync();

                    }
                }

                if (!storeDb.Set<DeliveryMethods>().Any())
                {
                    var DeliveryMethodsData = File.OpenRead(@"..\Infastructure\Persistence\Data\DataSeed\delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethods>>(DeliveryMethodsData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await storeDb.Set<DeliveryMethods>().AddRangeAsync(DeliveryMethods);
                        await storeDb.SaveChangesAsync();

                    }
                }

            }
            catch (Exception ex) { }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Ahmed@gmail.com",
                        DisplayName = "Ahmed Khaled",
                        PhoneNumber = "01118227172",
                        UserName = "AhmedKhaled"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Mohamedd@gmail.com",
                        DisplayName = "Khaled Ahmed",
                        PhoneNumber = "01020238429",
                        UserName = "Khaled Ahmed"
                    };
                    await userManager.CreateAsync(User01, "P@ssw0rd");
                    await userManager.CreateAsync(User02, "P@ssw0rd");
                    await userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await userManager.AddToRoleAsync(User02, "Admin");
                }
               await storeIdentity.SaveChangesAsync();
                
            }catch (Exception ex) { }
        }
    }
}
