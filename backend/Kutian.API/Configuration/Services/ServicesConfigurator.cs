using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Kutian.API.Configuration.Services;
using Kutian.Infrastructure.Features.Users.Queries;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core;
using Kutian.Utilities.Core.Extensions;
using Kutian.Utilities.Core.Filters;
using Kutian.Utilities.Core.Models;
using Kutian.Utilities.Core.Utils;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Configuration.Services
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Kutian API",
                    Description = "Kutian API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Wallruzz9114",
                        Email = "example@email.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });

                options.CustomSchemaIds(x => x.FullName);
            });

            services.ConfigureSwaggerGen(options =>
            {
                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder =>
                    {
                        builder
                            .WithOrigins("https://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(isOriginAllowed: _ => true)
                            .AllowCredentials();
                    }
                );
            });

            services.AddHttpContextAccessor();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenProvider, TokenProvider>();
            services.AddMediatR(typeof(Login));

            services.AddEventStore(new EventStoreBuilderOptions
            {
                ConnectionString = configuration["ConnectionStrings:DatabaseConnection"],
                MigrationAssembly = "Kutian.Utilities"
            });

            var issuerSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                configuration[$"{ nameof(AuthenticationSettings) }:{ nameof(AuthenticationSettings.JWTKey) }"]));

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler
            {
                InboundClaimTypeMap = new Dictionary<string, string>()
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(jwtSecurityTokenHandler);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigninKey,
                    ValidateIssuer = true,
                    ValidIssuer = configuration[$"{ nameof(AuthenticationSettings) }:{ nameof(AuthenticationSettings.JWTIssuer) }"],
                    ValidateAudience = true,
                    ValidAudience = configuration[$"{ nameof(AuthenticationSettings) }:{ nameof(AuthenticationSettings.JWTAudience) }"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = JwtRegisteredClaimNames.UniqueName
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Request.Query.TryGetValue("access_token", out StringValues token);
                        if (!string.IsNullOrEmpty(token)) context.Token = token;

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddControllers();
        }
    }
}