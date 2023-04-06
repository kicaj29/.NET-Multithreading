using System.Diagnostics;

namespace ExceptionHandlingInTasks.AspNetCore
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("CAUGHT BY ExceptionHandlerMiddleware :" + exc.Message);
                throw;
            }
        }
    }
}
