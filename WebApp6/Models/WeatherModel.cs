namespace WebApp6.Models
{
    public class WeatherModel
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureInC { get; set; }

        public int TemperatureInF => 32 + (int)(TemperatureInC / 0.5556);

        public string? Description { get; set; }
    }
}
