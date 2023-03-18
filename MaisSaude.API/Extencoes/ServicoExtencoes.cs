using AspNetCoreRateLimit;
using MaisSaude.Business.Rabbit;
using MaisSaude.Common;
using MaisSaude.Infra.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace MaisSaude.API.Extencoes
{
    public static class ServicoExtencoes
    {
        public static void ConfigurarJwt(this IServiceCollection services)
        {
            Environment.SetEnvironmentVariable("JWT_SECRETO",
                Convert.ToBase64String(new HMACSHA256().Key), EnvironmentVariableTarget.Process);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = "EmitenteDoJWT",
                        ValidAudience = "DestinatarioDoJWT",
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Convert.FromBase64String(Environment.GetEnvironmentVariable("JWT_SECRETO")))
                    };

                });

        }

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "post:/api/Login",
                    Limit = 2,
                    Period = "10s",
                },
                //new RateLimitRule
                //{
                //    Endpoint = "*",
                //    Period = "10s",
                //    Limit = 2
                //}
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.EnableEndpointRateLimiting = true;
                opt.StackBlockedRequests = false;
                opt.GeneralRules = rateLimitRules;
            });

            services.AddInMemoryRateLimiting();

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
        public static void ConfigurarSwagger(this IServiceCollection services) =>
             services.AddSwaggerGen(c =>
             {

                 c.SwaggerDoc("v1", new OpenApiInfo
                 {

                     Title = "Api - Claudio ",
                     Version = "v1",
                     Description = "Apis para cadastros"

                 });
                 c.EnableAnnotations();
                 var securityScheme = new OpenApiSecurityScheme
                 {
                     Name = "Autenticação JWT",
                     Description = "Informe o token JTW Bearer **_somente_**",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.Http,
                     Scheme = "bearer",
                     BearerFormat = "JWT",
                     Reference = new OpenApiReference
                     {
                         Id = JwtBearerDefaults.AuthenticationScheme,
                         Type = ReferenceType.SecurityScheme
                     }
                 };
                 c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {securityScheme, Array.Empty<string>() }
                 });
             });
        public static void ConfigurarServicos(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddCors();

            #region RabbitMQ
            services.Configure<DadosBaseRabbitMQ>(configuration.GetSection("DadosBaseRabbitMQ"));
            services.AddScoped<IMensageria, Mensageria>();
            services.AddSingleton<RabbitMQFactory>();
            #endregion

        }















    }
    }