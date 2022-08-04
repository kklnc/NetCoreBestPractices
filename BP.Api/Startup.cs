using BP.Api.Extensions;
using BP.Api.Models;
using BP.Api.Service;
using BP.Api.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api
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


            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });


            #region 3-)HealthCheck
            services.AddHealthChecks();
            #endregion

            #region 4-)DependencyInjection
            services.AddScoped<IContactService, ContactService>();
            #endregion

            #region 4-)Mapping DependencyInjection
            services.ConfigureMapping();
            #endregion

            #region 5-)Validation
            services.AddTransient<IValidator<ContactDTO>, ContactValidator>();
            #endregion

            #region 7-)IHttpClientFactory
            services.AddHttpClient("garantiapi", config =>
            {
                config.BaseAddress = new Uri("http://www.yourapiurl.com");
                config.DefaultRequestHeaders.Add("Authorization", "Bearer 1212122");
            });
            #endregion

            #region 8-)Log
            services.AddLogging();
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BP.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BP.Api v1"));
            }
            #region 3-)HealthCheck
            app.UseCustomHealthCheck();
            #endregion

            #region 6-)ResponseCaching
            app.UseResponseCaching();
            #endregion
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
