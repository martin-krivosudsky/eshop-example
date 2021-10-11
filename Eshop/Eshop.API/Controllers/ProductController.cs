using Eshop.Core.Models.DTO;
using Eshop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eshop.API.Controllers
{
    /// <summary>
    /// Controller for getting and updating Products.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService">Product service</param>
        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        }

        /// <summary>
        /// Gets all Products.
        /// </summary>
        /// <returns>List of products ordered by name.</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll().ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Gets multiple Products.
        /// </summary>
        /// <param name="skip">How many products to skip before taking</param>
        /// <param name="take">How many products to return</param>
        /// <returns>List of Products ordered by name</returns>
        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] int skip, [FromQuery] int take)
        {
            var result = await _productService.GetAll(skip, take).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Gets one product.
        /// </summary>
        /// <param name="productId">ID of desired product.</param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery] long productId)
        {
            var result = await _productService.Get(productId).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Edits description of one product.
        /// </summary>
        /// <param name="editDescriptionDTO">DTO object with ProductId and NewDescription</param>
        /// <returns></returns>
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
