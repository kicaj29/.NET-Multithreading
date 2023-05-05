using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly HttpClient _httpClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
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

        [HttpGet("FireAndForgetExceptionHandling")]
        public async Task<ActionResult> FireAndForgetExceptionHandling(HowToThrowWithoutAwait howToThrow)
        {
            try
            {
                switch (howToThrow)
                {
                    case HowToThrowWithoutAwait.RunNestedNoAsyncWithReturn:                 // returns 500 to the client
                        await Task.Run(() =>
                        {
                            // await Task.Yield(); // this will schedule thread pool with the rest of this method, TODO this as better example
                            return Task.Run(() =>                                           // current test is returned to parent task can await it
                            {
                                throw new Exception($"FireAndForgetExceptionHandling: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                            });
                        });
                        break;
                    case HowToThrowWithoutAwait.RunNestedNoAsyncNoReturn:                   // returns 200 to the client (exception is swallowed)
                        await Task.Run(() =>
                        {
                            Task.Run(() =>                                                  // there is no return so parent task cannot await child task
                            {
                                throw new Exception($"FireAndForgetExceptionHandling: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                            });
                        });
                        break;
                    case HowToThrowWithoutAwait.RunNestedAsyncWithReturn:                   // returns 200 to the client (exception is swallowed)
                        await Task.Run(async () =>
                        {
                            return Task.Run(() =>
                            {
                                throw new Exception($"FireAndForgetExceptionHandling: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                            });
                        });
                        break;
                    case HowToThrowWithoutAwait.RunNestedAsyncNoReturn:                     // returns 200 to the client (exception is swallowed)
                        await Task.Run(async () =>
                        {
                            Task.Run(() =>
                            {
                                throw new Exception($"FireAndForgetExceptionHandling: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                            });
                        });
                        break;
                    case HowToThrowWithoutAwait.RunSingle:                                  // returns 200 to the client (exception is swallowed)
                        Task.Run(() =>
                        {
                            throw new Exception($"FireAndForgetExceptionHandling: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                        });
                        break;
                    case HowToThrowWithoutAwait.StartNestedNoAsyncWithReturnNoUnwrap:       // returns 200 to the client (exception is swallowed)
                        await Task.Factory.StartNew(() =>
                        {
                            return Task.Run(() =>
                            {
                                throw new Exception($"GetStringThrowsAsync: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                            });
                        });
                        break;
                    case HowToThrowWithoutAwait.StartNestedNoAsyncWithReturnUnwrap:         // returns 500 to the client (Unwarap is not needed in case for Task.Run)
                        await Task.Factory.StartNew(() =>
                        {
                            return Task.Run(() =>
                            {
                                throw new Exception($"GetStringThrowsAsync: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                            });
                        }).Unwrap();
                        break;
                    case HowToThrowWithoutAwait.StartNestedAsyncWithReturnUnwrap:           // returns 200 to the client (exception is swallowed)
                        await Task.Factory.StartNew(async () =>
                        {
                            return Task.Run(() =>
                            {
                                throw new Exception($"GetStringThrowsAsync: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                            });
                        }).Unwrap();
                        break;
                    case HowToThrowWithoutAwait.StartNestedAsyncNoReturnUnwrap:             // returns 200 to the client (exception is swallowed)
                        await Task.Factory.StartNew(async () =>
                        {
                            Task.Run(() =>
                            {
                                throw new Exception($"GetStringThrowsAsync: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                            });
                        }).Unwrap();
                        break;
                    default:
                        throw new NotSupportedException();

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return Ok();
        }


        [HttpGet("TestNeedForCustomExceptionSerializationTasks")]
        public async Task<ActionResult> TestNeedForCustomExceptionSerializationTasks()
        {
            try
            {
                Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                await Task.Run(() =>
                {
                    Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    MyCustomException customEx = new MyCustomException("ABC");
                    Debug.WriteLine(customEx.GetHashCode());
                    throw customEx;
                });
            }
            catch (MyCustomException ex)
            {
                _logger.LogError(ex, ex.Message);
                Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine(ex.GetHashCode());
            }
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
            return Ok();
        }
    }
}