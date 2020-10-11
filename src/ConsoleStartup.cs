using System;
using System.Diagnostics.CodeAnalysis;
using BibleUpload.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleUpload
{
    [ExcludeFromCodeCoverage]
    public static class ConsoleStartup
    {
        public static IServiceProvider SetupDependencyInjection(IConfigurationRoot configuration)
        {
            return new ServiceCollection()
                .RegisterConfigurationOptions(configuration)
                .RegisterServices(configuration)
                .BuildServiceProvider(false);
        }

        public static IConfigurationRoot SetupConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var b = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

            return b.Build();
        }
    }
}