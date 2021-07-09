using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace codingtest.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGitHubService, GitHubService>();
         
            return services;
        }
    }
}
