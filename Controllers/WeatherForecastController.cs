using Microsoft.AspNetCore.Mvc;

namespace apisNET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    //Implementacion de logging
    private readonly ILogger<WeatherForecastController> _logger;

    //Creamos una lista nueva
    private static List<WeatherForecast> ListWeatherForecast = new List<WeatherForecast>();

    //Constructor, lo inicializamos con una lista de datos
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        //Si la lista es nula o no tiene ningún registro
        if(ListWeatherForecast == null || !ListWeatherForecast.Any()){

            ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
            .ToList();

        }
    }

    [HttpGet(Name = "GetWeatherForecast")]
    //[Route("Get/weatherforecast")]
    //[Route("Get/weatherforecast2")]
    //[Route("[action]")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogDebug("Retornando la lista de weatherforecast");
        return ListWeatherForecast;
    }

    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast)
    {
        ListWeatherForecast.Add(weatherForecast);
        return Ok();
    }

    [HttpDelete("{index}")]
    public IActionResult Put(int index) {
        try
        {
            ListWeatherForecast.RemoveAt(index);            
        }
        catch (ArgumentOutOfRangeException)
        {            
            return BadRequest(new { msg = $"Data doesn't exist at index: { index }"});
        }

        return Ok(new {msg = "Deleted!"});
    }
}
