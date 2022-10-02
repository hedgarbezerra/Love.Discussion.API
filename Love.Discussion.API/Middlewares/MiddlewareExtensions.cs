namespace Love.Discussion.API.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDefaultResponseMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<DefaultResponseMiddleware>();
    }
}
