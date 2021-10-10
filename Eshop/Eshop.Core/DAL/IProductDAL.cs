using Eshop.Core.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Core.DAL
{
    public interface IProductDAL
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(long id);
        Task EditDescription(long productId, string newDescription);
    }
}
