namespace Banking.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Events;
    using Serilog.Formatting.Compact;

    public class Program
    {
        public static void Main(string[] args)
        {
            AddLogger();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, builder) =>
                {
                    IHostingEnvironment env = context.HostingEnvironment;
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).
                        AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true,
                                                                          reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .UseStartup<Startup>()
                .Build();
        private static void AddLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(new CompactJsonFormatter(), "Logs/Logs.json", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

    }
}
