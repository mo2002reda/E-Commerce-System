using SkelandStore.Core.Entities;
using SkelandStore.Core.Entities.Order_Aggregation;
using SkyLand.Repository.Data;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SkyLand.Repository
{
    public static class StoreSeeding
    {
        //this will be helper class that help to upload data files
        //Seeding
        public static async Task SeedDataAsync(StoreDbContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())//Any check if database has any productBrands or not if exist return true 
            {
                #region Upload Brand Data
                //1)catch file of data at variable 
                //will get all brands as strings(do Serialize From Arrays of Brand To Strings)
                var BrandFile = File.ReadAllText("../SkyLand.Repository/Data/DataSeeding/brands.json");

                //2)do Deserilize To File which caught To return it to List Of Objects of Brands
                var Brand = JsonSerializer.Deserialize<List<ProductBrand>>(BrandFile);

                //3)loop for all Objects To Store them in dataBase
                if (Brand?.Count > 0)//check if Brand is not null && Count Of Brands > 0
                {
                    foreach (var item in Brand)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(item);//will add every Product Localy
                    }
                    await dbContext.SaveChangesAsync();//save all products in database
                }
                #endregion
            }

            if (!dbContext.ProductTypes.Any())
            {
                #region Types Seeding
                //1)Read The File
                //2)Deserilize The File
                //3)Loop on data To add it on database

                var ProductTypesFile = File.ReadAllText("../SkyLand.Repository/Data/DataSeeding/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesFile);
                if (Types?.Count > 0)
                {
                    foreach (var item in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
                #endregion
            }

            if (!dbContext.Products.Any())
            {
                #region Product Seeding
                //1)Read The File
                //2)Deserilize The File
                //3)Loop on data To add it on database

                var ProductData = File.ReadAllText("../SkyLand.Repository/Data/DataSeeding/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var item in Products)
                    {
                        await dbContext.Set<Product>().AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
                #endregion
            }

            if (!dbContext.DeleveryMethods.Any())
            {
                #region Product Seeding
                //1)Read The File
                //2)Deserilize The File
                //3)Loop on data To add it on database

                var DeleveryMethodData = File.ReadAllText("../SkyLand.Repository/Data/DataSeeding/delivery.json");
                var Delevery = JsonSerializer.Deserialize<List<DeleveryMethod>>(DeleveryMethodData);
                if (Delevery?.Count > 0)
                {
                    foreach (var item in Delevery)
                    {
                        await dbContext.Set<DeleveryMethod>().AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
                #endregion
            }

        }
    }
}
