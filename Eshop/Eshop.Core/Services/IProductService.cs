using Eshop.Core.Models.Database;
using Eshop.Core.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(long id);
        Task EditDescription(long productId, string newDescription);
    }
}
