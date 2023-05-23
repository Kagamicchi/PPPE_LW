using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp6.Services.V3;

namespace WebApp6.Controllers.V3
{
    [ApiVersion("3.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v3")]
    [Route("api/v3/[controller]")]
    public class ApiVersionController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public ApiVersionController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Get()
        {
            var workbook = await _excelService.Get();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "HelloWorld.xlsx");
        }
    }
}
