using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp6.Services.V1;

namespace WebApp6.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = true)]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v1/[controller]")]
    public class ApiVersionController : ControllerBase
    {
        private readonly INumberService _numberService;

        public ApiVersionController(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpGet, Authorize]
        [Obsolete("This method is deprecated in version 1.0! Please, use the updated version")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _numberService.GetRandomInteger());
        }
    }
}
