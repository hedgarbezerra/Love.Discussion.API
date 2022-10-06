using Love.Discussion.API.Versioning;
using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Interfaces;
using Love.Discussion.Repository.Context;
using Love.Discussion.Repository.Repositories;
using Love.Discussion.Services;
using Love.Discussion.Services.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.API
{
    public static class InternalServicesBinder
    {
        public static void AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

                return new UriService(uri);
            });

            services.AddTransient<IRepository<Complain>, ComplainRepository>();
            services.AddTransient<IRepository<Meeting>, MeetingRepository>();
            services.AddTransient<IMeetingService, MeetingService>();
            services.AddTransient<IUserService, UserService>();

            services.AddSingleton<IHashing, Hashing>();
            services.AddSingleton<IEncryption, Encryption>(_ => new Encryption(configuration["EncryptionKey"]));

        }
        public static void AddConfigurations(this IServiceCollection services, ConfigurationManager configurations)
        {
            //var dbConnectionString = configurations.GetConnectionString("SQLDb");
            //services.AddDbContext<LoveContext>(opt => opt.UseSqlServer(dbConnectionString));

            var identityDbConnectionString = configurations.GetConnectionString("SQLUsersDb");
            services.AddDbContext<LoveIdentityContext>(opt => opt.UseSqlServer(identityDbConnectionString))
                .AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                .AddEntityFrameworkStores<LoveIdentityContext>();
        }

        public static void AddInternalVersioning(this IServiceCollection services)
        {

            #region Versionamento da API (Considerada pelo header ou pela query string)
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            #endregion

            #region Swagger documentation setup
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionConfiguration>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerConfiguration>();
            });
            #endregion
        }

        public static void AddInternalAuthencation(this IServiceCollection services, IConfiguration configuration)
        {
            #region JWT token bearer and authentication setup

            byte[] tokenKeyBytes = Encoding.ASCII.GetBytes(configuration["TokenKey"] ?? string.Empty);
            if(tokenKeyBytes is not null && tokenKeyBytes.Length > 0)
            {
                var hostingEnvironment = services.BuildServiceProvider().GetService<IWebHostEnvironment>();

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.IncludeErrorDetails = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(tokenKeyBytes),
                        ValidateIssuer = true,
                        ValidIssuer = hostingEnvironment.ApplicationName,
                        ValidateAudience = false,
                        ValidateTokenReplay = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            }
           

            #endregion
        }
    }
}
