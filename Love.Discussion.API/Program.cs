using Love.Discussion.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Serilog;
using Serilog.Context;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Love.Discussion.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var hostingEnvironment = builder.Services.BuildServiceProvider().GetService<IWebHostEnvironment>();

            #region Configurations
            var configurations = builder.Configuration;

            var appConfigurationsConnectionString = configurations.GetConnectionString("AppConfig");
            configurations.AddAzureAppConfiguration((config) =>
            {
                config.Connect(appConfigurationsConnectionString)
                    .UseFeatureFlags()
                    .ConfigureRefresh(opt =>
                    {
                        opt.Register(AppConstants.AZURE_CONFIG_CACHE_SENTINEL, true);
                        opt.SetCacheExpiration(AppConstants.DEFAULT_CACHE_EXPIRACY);
                    });

                config.Select(KeyFilter.Any, LabelFilter.Null);
                config.Select(KeyFilter.Any, hostingEnvironment.EnvironmentName);
            });

            builder.Services.AddAzureAppConfiguration();
            builder.Services.AddConfigurations(configurations);
            #endregion

            #region services binding
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddInternalServices(configurations);
            builder.Services.AddInternalVersioning();
            builder.Services.AddInternalAuthencation(configurations);
            builder.Services.AddFeatureManagement()
                .AddFeatureFilter<TimeWindowFilter>();
            builder.Services.AddCors();
            #endregion

            #region Defining content negotiation
            builder.Services.AddControllers(op =>
            {
                op.RespectBrowserAcceptHeader = true;
                op.ReturnHttpNotAcceptable = true;
            })
                .AddXmlSerializerFormatters()
                .AddJsonOptions(ops =>
                {
                    ops.JsonSerializerOptions.PropertyNamingPolicy = null;
                    ops.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
                    ops.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    ops.JsonSerializerOptions.WriteIndented = true;
                    ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    ops.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    ops.JsonSerializerOptions.MaxDepth = 64;
                    ops.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            #endregion

            #region Build configuration
            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAzureAppConfiguration();
            app.UseAuthorization();
            app.MapControllers();
            app.UseStaticFiles();

            #region setting up logging 

            Log.Logger = new LoggerConfiguration()
                      .Enrich.FromLogContext()
                      .WriteTo.File(hostingEnvironment.WebRootPath + "\\logs\\log_.txt", rollingInterval: RollingInterval.Minute)
                      .CreateLogger();
            #endregion

            #region Setting up exception handling
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(c => c.Run(async context =>
                {
                    var exception = context.Features
                        .Get<IExceptionHandlerPathFeature>()?
                        .Error;

                    if (exception is null)
                        return;

                    var response = new { message = exception.Message, stacktrace = exception.StackTrace };
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }));
            }
            app.Use(async (httpContext, next) =>
            {
                var userName = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : string.Empty;
                LogContext.PushProperty("Username", userName);
                await next.Invoke();
            });
            #endregion

            #region Setup Swagger
            IApiVersionDescriptionProvider apiVersioningProvider = builder.Services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in apiVersioningProvider?.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
            #endregion

            app.Run();
            #endregion
        }
    }
}