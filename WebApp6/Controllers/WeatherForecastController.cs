using Microsoft.AspNetCore.Mvc;
using WebApp6.Models;

namespace WebApp6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Descriptions = new[]
        {
            "Balmy", "Dog days", "Sunny", "Tropical", "Bleak", "Crisp", "Frosty", "Icy", "Snowy", "Calm", "Clear", "Foul"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET WeatherForecast
        [HttpGet(Name = "GetWeatherModel")]
        public IEnumerable<WeatherModel> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherModel
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureInC = Random.Shared.Next(-20, 55),
                Description = Descriptions[Random.Shared.Next(Descriptions.Length)]
            })
            .ToArray();
        }

        // POST WeatherForecast
        [HttpPost]
        public async Task<IActionResult> Post(string? description)
        {
            return NotFound();
        }

        // PUT WeatherForecast
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int? id, string? description)
        {
            return NotFound();
        }

        // DELETE WeatherForecast
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return NotFound();
        }
    }
}
