using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace ConfigureAwaitInAspNet.Controllers
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
        private readonly HttpClient _httpClient;

        public WeatherForecastController(HttpClient httpClient, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet(Name = "TestConfigureAwait")]
        public async Task<ActionResult<string>> TestConfigureAwait(bool continueOnCapturedContext)
        {
            // It does not make sense to use ConfigureAwait in ASP.NET Core and newer versions because they do not have SynchronizationContext,
            // thread used to executed code after await is always another thread.
            // In case of WinForms by default code executed after await is always the same thread that was used before await (it is main UI thread).
            Debug.WriteLine($"ManagedThreadId before await: {Thread.CurrentThread.ManagedThreadId}");
            using (var httpResonse = await _httpClient.GetAsync("https://www.bynder.com").ConfigureAwait(continueOnCapturedContext))
            {
                Debug.WriteLine($"ManagedThreadId after await: {Thread.CurrentThread.ManagedThreadId}");
                return await httpResonse.Content.ReadAsStringAsync();
            }
        }
    }
}