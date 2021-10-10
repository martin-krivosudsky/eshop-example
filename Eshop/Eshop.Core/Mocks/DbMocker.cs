using Eshop.Core.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Core.Mocks
{
    public static class DbMocker
    {
        public static EshopDbContext InitializeTestDatabase(this EshopDbContext context)
        {
            context.Products.Add(new Product
            {
                Name = "Product 1",
                ImgUri = "",
                Price = 550
            });
            context.Products.Add(new Product
            {
                Name = "Product 2",
                ImgUri = "exampleUri",
                Price = 1200
            });
            context.Products.Add(new Product
            {
                Name = "Product 3",
                ImgUri = "",
                Price = 75,
                Description = "Optional description"
            });

            context.SaveChanges();

            return context;
        }

        public static EshopDbContext TruncateTables(this EshopDbContext context)
        {
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Products]");

            return context;
        }
    }
}
