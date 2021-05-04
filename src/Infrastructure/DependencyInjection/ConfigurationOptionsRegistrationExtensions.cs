using System.Diagnostics.CodeAnalysis;
using BibleUpload.Infrastructure.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleUpload.Infrastructure.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ConfigurationOptionsRegistrationExtensions
    {
        public static IServiceCollection RegisterConfigurationOptions(
            this ServiceCollection serviceCollection,
            IConfigurationRoot configuration)
        {
            serviceCollection.AddSingleton<IConfiguration>(x => configuration);

            serviceCollection.AddOptions();

            serviceCollection.AddOptions<SqliteConfiguration>()
                .Bind(configuration.GetSection(ConfigurationConstants.Sqlite));
            
            return serviceCollection;
        }
    }
}