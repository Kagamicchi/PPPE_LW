using Microsoft.AspNetCore.Mvc;
using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.History;
using WebApp6.Services.HistoryService;

namespace WebApp6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<CategoryModel>>> Get(Guid id)
        {
            var category = await _categoryService.Get(id);
            if (category == null || category.ValueCount == 0)
            {
                return NotFound(category);
            }

            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<CategoryModel>>> GetAll()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CategoryModel>>> Post([FromBody] CategoryRequest request)
        {
            var category = await _categoryService.Post(request);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<CategoryModel>>> Put(Guid id, [FromBody] CategoryModel category)
        {
            var response = await _categoryService.Put(id, category);

            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<CategoryModel>>> Delete(Guid id)
        {
            await _categoryService.Delete(id);
            return NoContent();
        }
    }
}
