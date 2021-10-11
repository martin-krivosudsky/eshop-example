using Eshop.Core.Models.DTO;
using Eshop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eshop.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll().ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] int skip, [FromQuery] int take)
        {
            var result = await _productService.GetAll(skip, take).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery] long productId)
        {
            var result = await _productService.Get(productId).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [Route("edit-description")]
        public async Task<IActionResult> EditDescription([FromForm] EditDescriptionDTO editDescriptionDTO)
        {
            await _productService.EditDescription(editDescriptionDTO.ProductId, editDescriptionDTO.NewDescription).ConfigureAwait(false);

            return Ok();
        }
    }
}
