using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExceptionHandlingInTasks.AspNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetStringThrows")]
        public string GetStringThrows()
        {
            throw new Exception("GetStringThrows");
        }

        [HttpGet("GetStringThrowsAsync")]
        public async Task<ActionResult<string>> GetStringThrowsAsync()
        {
            string result = null;
            try
            {
                result = await Task.Run<string>(() =>
                {
                    throw new Exception($"GetStringThrowsAsync: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                    return "abc";
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
    }
}