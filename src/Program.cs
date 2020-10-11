using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BibleUpload.Infrastructure.Extensions;
using BibleUpload.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleUpload
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        private static IServiceProvider ServiceProvider { get; set; }

        public static async Task Main(string[] args)
        {
            const string consoleAppOperation = "Bible Uploader";
            var watch = Stopwatch.StartNew();
            var exitCode = 0;

            ConsoleExtensions.PrintStartMessage(consoleAppOperation);

            Configuration = ConsoleStartup.SetupConfiguration();
            ServiceProvider = ConsoleStartup.SetupDependencyInjection(Configuration);

            try
            {
                using var scope = ServiceProvider.CreateScope();
                var serviceProvider = scope.ServiceProvider;
                var serviceRunner = serviceProvider.GetRequiredService<IServiceRunner>();
                
                await serviceRunner.Run();


                Console.WriteLine("derp");
            }
            catch (Exception e)
            {
                ConsoleExtensions.PrintError($"\n {e} \n");
                exitCode = -1;
            }
            finally
            {
                watch.Stop();

                ConsoleExtensions.PrintExitMessage(consoleAppOperation, exitCode, watch);
            }
        }
    }
}