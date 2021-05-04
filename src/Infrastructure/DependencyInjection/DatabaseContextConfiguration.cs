using BibleUpload.Data.Context;
using BibleUpload.Data.Context.Interfaces;
using BibleUpload.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleUpload.Infrastructure.DependencyInjection
{
    public static class DatabaseContextConfiguration
    {
        public static void AddDatabaseContexts(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddEsvBibleContext(configuration);
        }

        public static void AddEsvBibleContext(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var sqliteConfig = configuration
                .GetSection("Sqlite")
                .Get<SqliteConfiguration>();

            serviceCollection.AddDbContext<EsvBibleContext>(builder =>
            {
                /*builder.UseNpgsql(configuration.GetConnectionString(ConnectionStringConstants.ThriveDbConnection));*/
                builder.UseSqlite(sqliteConfig.Location);
            });

            serviceCollection.AddScoped<IEsvBibleContext, EsvBibleContext>();
        }
    }
}