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
            if(context.Request.ContentType.Contains("json", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.WriteAsync("Hello  classe Middleware");
            }

            await _next(context);
        }
    }
}
