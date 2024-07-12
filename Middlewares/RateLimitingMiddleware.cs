using Microsoft.AspNetCore.Http.HttpResults;

namespace MwTesting.Middlewares
{
    public class RateLimitingMiddleware
    {
        private static int _counter = 0;
        private static DateTime _lastRequesteDate = DateTime.Now;
        private readonly RequestDelegate _next;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            _counter++;

            if (DateTime.Now.Subtract(_lastRequesteDate).Seconds > 1)
            {
                _counter = 1;
                _lastRequesteDate = DateTime.Now;
                await _next(context);
            }
            else
            {
                if (_counter > 5)
                {
                    _lastRequesteDate = DateTime.Now;
                    await context.Response.WriteAsync("Rate Limte exceeded");
                }
                else
                {
                    _lastRequesteDate = DateTime.Now;
                    await _next(context);
                }
            }
        }
    }
}