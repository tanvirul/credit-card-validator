using CreditCardValidator.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CreditCardValidator.Infrastructure.Installers
{
    public class RegisterSwagger : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer ASP.NET Core API", Version = "v1" });

            });
        }
    }
}
