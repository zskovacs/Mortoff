using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Mortoff.Common;
public static class InstallerExtension
{
    public static void InstallServicesInAssembly(this IServiceCollection service, IConfiguration configuration)
    {
        var installers = GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        installers.ForEach(i => i.InstallServices(service, configuration));
    }

    private static Assembly[] GetAssemblies()
    {
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
        return assemblies.ToArray();
    }
}
