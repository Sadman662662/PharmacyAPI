using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration) 
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
               typeof(IInstallers).IsAssignableFrom(x) &&
               !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance)
               .Cast<IInstallers>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
       
    }
}
