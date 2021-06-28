using LoginSystem.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Services
{
    public static class ServiceResolver
    {
        public static void RegistrateServicesResolver(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISecurityService, SecurityService>();
            //services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<MemoryCacheSingleton>();
        }
    }
}
