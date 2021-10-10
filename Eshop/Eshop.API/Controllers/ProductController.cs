using Eshop.Core.Models.DTO;
using Eshop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll().ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery] long productId)
        {
            var result = await _productService.Get(productId).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost]
        [Route("edit-description")]
        public async Task<IActionResult> EditDescription([FromForm] EditDescriptionDTO editDescriptionDTO)
        {
            await _productService.EditDescription(editDescriptionDTO.ProductId, editDescriptionDTO.NewDescription).ConfigureAwait(false);

            return Ok();
        }
    }
}
