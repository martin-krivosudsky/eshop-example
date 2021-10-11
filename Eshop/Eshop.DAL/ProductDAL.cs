using Eshop.Core;
using Eshop.Core.DAL;
using Eshop.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.DAL
{
    public class ProductDAL : IProductDAL
    {
        private readonly EshopDbContext _dbContext;

        public ProductDAL(EshopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        public async Task EditDescription(long productId, string newDescription)
        {
            Product product = await _dbContext.FindAsync<Product>(productId).ConfigureAwait(false);

            if (product != null)
            {
                product.Description = newDescription;
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<Product> Get(long id)
        {
            return await _dbContext.FindAsync<Product>(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products.OrderBy(p => p.Name).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetAll(int skip, int take)
        {
            return await _dbContext.Products.OrderBy(p => p.Name).Skip(skip).Take(take).ToListAsync().ConfigureAwait(false);
        }
    }
}
