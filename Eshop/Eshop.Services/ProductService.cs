using Eshop.Core.DAL;
using Eshop.Core.Models.Database;
using Eshop.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDAL _productDAL;

        public ProductService(IProductDAL productDAL)
        {
            _productDAL = productDAL ?? throw new ArgumentNullException(nameof(productDAL));
        }

        public async Task EditDescription(long productId, string newDescription)
        {
            await _productDAL.EditDescription(productId, newDescription).ConfigureAwait(false);
        }

        public async Task<Product> Get(long id)
        {
            return await _productDAL.Get(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productDAL.GetAll().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetAll(int skip, int take)
        {
            return await _productDAL.GetAll(skip, take).ConfigureAwait(false);
        }
    }
}
