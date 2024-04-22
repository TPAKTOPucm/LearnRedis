using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace LearnRedis.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IDistributedCache _cache;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    [HttpGet]
    public IActionResult Forecast(string id)
    {
        return Ok(JsonSerializer.Deserialize<WeatherForecast>(_cache.GetStringAsync(id).Result));
    }

    [HttpPost]
    public IActionResult Forecast(string id, [FromBody] WeatherForecast forecast)
    {
        _cache.SetStringAsync(id, JsonSerializer.Serialize(forecast));
        return Ok();
    }
}
