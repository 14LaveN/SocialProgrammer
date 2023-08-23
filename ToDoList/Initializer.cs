using Microsoft.AspNetCore.Cors.Infrastructure;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.DAL.Repositories;
using SocialProgrammer.Service.Implementations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SocialProgrammer
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddSingleton<UserService>();
        }
    }
}
