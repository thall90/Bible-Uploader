using System.Diagnostics.CodeAnalysis;
using BibleUpload.Infrastructure.Extensions;
using BibleUpload.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleUpload.Infrastructure.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class MyRegistrationExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAssemblyTypes(
                typeof(CsvParserService),
                lifetime: ServiceLifetime.Transient);

            services.AddDatabaseContexts(configuration);

            return services;
        }
    }
}