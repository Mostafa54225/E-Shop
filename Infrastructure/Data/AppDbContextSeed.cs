using System.Text.Json;
using Core.Models.Product;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProdcutTypes.Any())
                {
                    Console.WriteLine("Seeding ProdcutTypes");
                    var productTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProdcutType>>(productTypesData);
                    foreach (var type in productTypes)
                    {
                        context.ProdcutTypes.Add(type);
                    }
                }
                if (!context.ProductBrands.Any())
                {
                    Console.WriteLine("Seeding ProductBrands");
                    var productBrandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    foreach (var brand in productBrands)
                    {
                        Console.WriteLine(brand);
                        context.ProductBrands.Add(brand);
                    }
                }
                if (!context.Products.Any())
                {
                    Console.WriteLine("Seeding Products");
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("test");
                Console.WriteLine(ex.Message);
                var logger = loggerFactory.CreateLogger<AppDbContext>();
                logger.LogError(ex.Message);
            }

        }
    }
}