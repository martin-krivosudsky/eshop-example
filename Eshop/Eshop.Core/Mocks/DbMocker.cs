using Eshop.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace Eshop.Core.Mocks
{
    public static class DbMocker
    {
        public static EshopDbContext InitializeTestDatabase(this EshopDbContext context)
        {
            Random rnd = new Random();
            for (int i = 1; i <= 200; i++)
            {
                context.Products.Add(new Product
                {
                    Name = $"Product-{i}",
                    ImgUri = "https://www.eppendorf.com/fileadmin/_processed_/4/5/csm_Shop_ICON_Final_28d0afae6a.jpg",
                    Price = rnd.Next(10, 10000)
                });
            }

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
