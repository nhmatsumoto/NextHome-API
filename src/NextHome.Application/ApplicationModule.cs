using Microsoft.Extensions.DependencyInjection;
using NextHome.Application.UseCases.Properties;
using NextHome.Application.UseCases.Properties.Interfaces;
using NextHome.Application.UseCases.Users;
using NextHome.Application.UseCases.Users.Interfaces;

namespace NextHome.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Users
            services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
            services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

            // Properties
            services.AddScoped<IGetAllPropertiesUseCase, GetAllPropertiesUseCase>();
            services.AddScoped<IGetPropertyByIdUseCase, GetPropertyByIdUseCase>();
            services.AddScoped<ICreatePropertyUseCase, CreatePropertyUseCase>();
            services.AddScoped<IUpdatePropertyUseCase, UpdatePropertyUseCase>();
            services.AddScoped<IDeletePropertyUseCase, DeletePropertyUseCase>();

            return services;
        }
    }
}
