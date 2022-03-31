using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mortoff.Application.Interfaces;
using Mortoff.Common;

namespace Mortoff.Service.Import;
internal class ImportInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration) => services.AddScoped(typeof(IFileParser<>), typeof(CsvFileParser<>));
}
