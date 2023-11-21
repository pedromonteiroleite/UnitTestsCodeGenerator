using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(SASProviderIS.Startup))]

namespace SASProviderIS
{
    public class Startup : FunctionsStartup
    {

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            string cs = Environment.GetEnvironmentVariable("ConnectionString");
            // builder.ConfigurationBuilder.AddAzureAppConfiguration(cs);
            //builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
            //{
            //    options.Connect(
            //            builder.Configuration["ConnectionStrings:AppConfig"])
            //        .ConfigureKeyVault(kv =>
            //        {
            //            kv.SetCredential(new DefaultAzureCredential());
            //        });
            //});
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            /*
            Transient: 
                Transient services are created upon each resolution of the service.
            Scoped: 
                The scoped service lifetime matches a function execution lifetime. Scoped services are created once per function execution. Later requests for that service during the execution reuse the existing service instance.
            Singleton: 
                The singleton service lifetime matches the host lifetime and is reused across function executions on that instance. Singleton lifetime services are recommended for connections and clients, for example DocumentClient or HttpClient instances.
             */

            //builder.Services.AddHttpClient();
            //builder.Services.AddOptions<MyOptions>()
            //    .Configure<IConfiguration>((settings, configuration) =>
            //    {
            //        configuration.GetSection("MyOptions").Bind(settings);
            //    });

            //builder..Configuration.AddAzureAppConfiguration(options =>
            //{
            //    options.Connect(
            //            builder.Configuration["ConnectionStrings:AppConfig"])
            //        .ConfigureKeyVault(kv =>
            //        {
            //            kv.SetCredential(new DefaultAzureCredential());
            //        });
            //});

            // Sample
            //builder.Services.AddSingleton<IMyService>((s) => {
            //    return new MyService();
            //});

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}
