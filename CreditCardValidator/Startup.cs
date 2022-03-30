using AutoWrapper;
using CreditCardValidator.Filters;
using CreditCardValidator.Infrastructure.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CreditCardValidator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServicesInAssembly(Configuration);
            services.AddControllers()
                    .AddNewtonsoftJson(ops => { ops.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; })
                    .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; });

            services.AddScoped<CreditCardValidationFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core API v1");
            });
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { IsDebug = false, UseApiProblemDetailsException = true });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
