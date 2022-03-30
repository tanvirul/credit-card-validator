using CreditCardValidator.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CreditCardValidator.Infrastructure.Extensions
{
    public static class ServiceRegistrationExtensioncs
    {
        public static void AddServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var appServices = typeof(Startup).Assembly.DefinedTypes
                            .Where(x => typeof(IServiceRegistration)
                            .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                            .Select(Activator.CreateInstance)
                            .Cast<IServiceRegistration>().ToList();

            appServices.ForEach(svc => svc.RegisterAppServices(services, configuration));
        }
    }
}
