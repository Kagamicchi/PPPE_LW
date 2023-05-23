using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp6.Services.V2;

namespace WebApp6.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v2/[controller]")]
    public class ApiVersionController : ControllerBase
    {
        private readonly IStringService _stringService;

        public ApiVersionController(IStringService stringService)
        {
            _stringService = stringService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _stringService.GetSomeText());
        }
    }
}
