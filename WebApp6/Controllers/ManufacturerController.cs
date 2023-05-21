using Microsoft.AspNetCore.Mvc;
using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.Manufacturer;
using WebApp6.Services.ManufacturerService;

namespace WebApp6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<ManufacturerModel>>> Get(Guid id)
        {
            var manufacturer = await _manufacturerService.Get(id);
            if (manufacturer == null || manufacturer.ValueCount == 0)
            {
                return NotFound(manufacturer);
            }

            return Ok(manufacturer);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<ManufacturerModel>>> GetAll()
        {
            return Ok(await _manufacturerService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<ManufacturerModel>>> Post([FromBody] ManufacturerRequest request)
        {
            var manufacturer = await _manufacturerService.Post(request);
            return Ok(manufacturer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<ManufacturerModel>>> Put(Guid id, [FromBody] ManufacturerModel manufacturer)
        {
            var response = await _manufacturerService.Put(id, manufacturer);

            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<ManufacturerModel>>> Delete(Guid id)
        {
            await _manufacturerService.Delete(id);
            return NoContent();
        }
    }
}
