using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Employee.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Infrastructure.Employee;
using Domain.Employee;
using EmployeeApi.Middleware;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using static Infrastructure.Employee.Constants;
using EmployeeApi.Validators;
using FluentValidation.AspNetCore;
namespace EmployeeApi
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
            string employeeConnectionstring = Configuration.GetConnectionString("EmployeeConnectionstring");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web));
            services.AddScoped<IEmployeeAdd, EmployeeAdder>();
            services.AddScoped<IEmployeeReader, EmployeeReader>();
            services.AddControllers()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EmployeeAddRequestValidator>());
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(employeeConnectionstring));
            services.AddApiVersioning(x =>
            {
                x.ReportApiVersions = true;
                x.ApiVersionReader = new HeaderApiVersionReader(VersionHeaderKey);
            });
            services.AddVersionedApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'VVV";
                x.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen();
            services.AddAuthentication();
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                }
            );
            }
            app.UseWhen(context => !ShouldExclue(context), appBuilder => appBuilder.UseMiddleware<AuthenticationHandler>());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private bool ShouldExclue(HttpContext context)
        {
            return context.Request.Path.Value.Contains("/swagger")
                || context.Request.Path.Value.Equals("/");
        }
    }
}
