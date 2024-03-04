using CacauShow.API.Data.Repository;
using CacauShow.API.Domain.Interfaces;
using CacauShow.API.Domain.Services;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace CacauShow.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICachingRepository, CachingRepository>();

            services.AddAuthentication(
                    CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();

            return services;
        }
    }
}
