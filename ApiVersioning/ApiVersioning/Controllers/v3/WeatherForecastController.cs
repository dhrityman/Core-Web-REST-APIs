using ApiVersioning.Entities;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
//Step 38
namespace ApiVersioning.Controllers.v3
{
    /// <summary>
    /// This controller provides weather forecast data.
    /// [ApiController]:=> Indicates that this class is an API controller, enabling features like automatic model validation and binding.
    /// [Route("[controller]")]:=> Specifies the route template for the controller, where "[controller]" is replaced with the controller's name (in this case, "WeatherForecast").
    /// </summary>
    [ApiController]
    //[Route("[controller]")]
    [Route("api/WeatherForecast")] //Step 43
    [Route("api/v{version:apiVersion}/[controller]")] //Step 35
    [ApiVersion("3.0")] //Step 39
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
