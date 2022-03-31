using Microsoft.EntityFrameworkCore;
using Mortoff.Persistence;
using Serilog;

namespace Mortoff.WebUI;

public class Program
{

    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables()
        .AddUserSecrets<Startup>(true)
        .Build();

    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

        try
        {
            Log.Information("Initializing...");

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var appDbContext = services.GetRequiredService<AppDbContext>();
                    if (appDbContext.Database.IsSqlServer())
                    {
                        Log.Information("Migrating AppDbContext...");
                        appDbContext.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while migrating or initializing the database.");
                }
            }

            Log.Information("Starting Host...");
            host.Run();

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog();
}
