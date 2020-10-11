using System.Diagnostics.CodeAnalysis;
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

            return serviceCollection;
        }
    }
}