using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eeShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eeShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // // CreateHostBuilder(args).Build().SeedDatabase().Run();
            // //Create Conmection
            // var connectionStringBuilder = new SqliteConnectionStringBuilder();
            // connectionStringBuilder.DataSource = "./sqlite/eshop.db";

            // using(var connection = new SqliteConnection(connectionStringBuilder.ConnectionString)){
            //     connection.Open();

            // }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class IHostExtenions{
        public static IHost SeedDatabase(this IHost host){
             var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
             
            using var scope = scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ProductContext>();

            if (context.Database.EnsureCreated())
                SeedData.Initialize(context);

            return host;
        }
    }
}
