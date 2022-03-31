using Mortoff.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mortoff.Application.Interfaces;

namespace Mortoff.Persistence;
public class PersistenceInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(defaultConnectionString));

        services.AddScoped<IAppdDbContext>(provider => provider.GetService<AppDbContext>());

        services.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(defaultConnectionString));
    }
}
