using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NextHome.Application;
using NextHome.Application.Mappings;
using NextHome.Application.Validators;
using NextHome.Domain.Interfaces;
using NextHome.Infrastructure.Repositories;

namespace NextHome.Infrastructure.IoC
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("NextHomeCorsPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // Adiciona autorização
            services.AddAuthorization();

            // Keycloak Configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://localhost:8080/realms/next-home";
                    options.Audience = "next-home-client";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Headers["Authorization"];
                            Console.WriteLine($"Token Recebido no Backend: {token}");
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Erro na Autenticação: {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };
                });

            //Application 
            services.AddApplicationServices();

            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            // Fluent Validation
            // Registra automaticamente todos os validadores da camada Application
            services.AddValidatorsFromAssemblyContaining<PropertyValidator>();

            // Add FluentValidation to API pipeline
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            // AutoMapper
            services.AddAutoMapper(typeof(PropertyMappingProfile));

            // Controllers
            services.AddControllers();


            return services;
        }
    }
}
