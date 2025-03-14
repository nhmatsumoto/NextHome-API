using Microsoft.Extensions.DependencyInjection;
using NextHome.Application.Interfaces.Properties;
using NextHome.Application.UseCases.Properties;

namespace NextHome.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGetAllPropertiesUseCase, GetAllPropertiesUseCase>();
            services.AddScoped<IGetPropertyByIdUseCase, GetPropertyByIdUseCase>();
            services.AddScoped<ICreatePropertyUseCase, CreatePropertyUseCase>();
            services.AddScoped<IUpdatePropertyUseCase, UpdatePropertyUseCase>();
            services.AddScoped<IDeletePropertyUseCase, DeletePropertyUseCase>();

            return services;
        }
    }
}
