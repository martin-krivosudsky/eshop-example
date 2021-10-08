using Eshop.Core.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Database
{
    public class EshopDbContext : DbContext
    {
        public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}