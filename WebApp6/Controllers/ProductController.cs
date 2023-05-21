using Microsoft.AspNetCore.Mvc;
using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.Product;
using WebApp6.Services.ProductService;

namespace WebApp6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<ProductModel>>> Get(Guid id)
        {
            var product = await _productService.Get(id);
            if (product == null || product.ValueCount == 0)
            {
                return NotFound(product);
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<ProductModel>>> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<ProductModel>>> Post([FromBody] ProductRequest request)
        {
            var product = await _productService.Post(request);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<ProductModel>>> Put(Guid id, [FromBody] ProductModel product)
        {
            var response = await _productService.Put(id, product);

            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<ProductModel>>> Delete(Guid id)
        {
            await _productService.Delete(id);
            return NoContent();
        }
    }
}
