using Eshop.Core.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetAll(int skip, int take);
        Task<Product> Get(long id);
        Task EditDescription(long productId, string newDescription);
    }
}
