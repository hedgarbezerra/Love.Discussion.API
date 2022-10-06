using Newtonsoft.Json;

namespace Love.Discussion.API.Middlewares
{
    public class DefaultResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public DefaultResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var contentyType = context.Request?.ContentType;
            if (contentyType is not null && contentyType.Contains("json", StringComparison.CurrentCultureIgnoreCase))
            {
                //ler resultado
                //pega ele como json
                //
                context.Response.WriteAsync("Hello  classe Middleware");
            }

            await _next(context);
        }
    }
}
