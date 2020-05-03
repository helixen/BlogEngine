using System;
using Api.Application.Contracts.Services;
using Api.Application.Services;
using Api.DataAccess;
using Api.DataAccess.Contracts;
using Api.DataAccess.Contracts.Repositories;
using Api.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Common.Register
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            AddRegisterServices(services);
            AddRegisterRepositories(services);

            return services;
        }
        private static IServiceCollection AddRegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
        private static IServiceCollection AddRegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IApiDBContext, ApiDBContext>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();


            return services;
        }
    }
}
