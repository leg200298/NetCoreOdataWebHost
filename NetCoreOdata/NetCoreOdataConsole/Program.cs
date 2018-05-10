using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using Microsoft.AspNet.OData;

namespace NetCoreOdataConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                 .UseContentRoot(Directory.GetCurrentDirectory())
                 .UseIISIntegration()
                 .UseStartup<Startup>()
                 .UseHttpSys(options =>
                 {
                     options.Authentication.Schemes = AuthenticationSchemes.None;
                     options.Authentication.AllowAnonymous = true;
                     options.MaxConnections = 100;
                     options.UrlPrefixes.Add("http://+:32767/");
                 })
                 .Build();

            host.Run();
        }
    }
    class Startup
    {
        private IConfiguration _configuration;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");

            _configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<DBContext>(options => options.UseMySQL(_configuration["MysqlConnectionString"]));
            services.AddOData();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Title = "OGC Odata API",
                    Version = "v1"
                });
                c.OperationFilter<SwaggerDefaultValues>();

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, $"NetCoreOdataConsole.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(cors =>
             cors.AllowAnyOrigin()
            );

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sensor Thing v1");
            });


            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Sensor>(nameof(Sensor));
            builder.EntitySet<Thing>(nameof(Thing));

            app.UseMvc(route =>
            {
                route.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
                route.MapODataServiceRoute("OdataRoute", "Odata", builder.GetEdmModel());
                route.EnableDependencyInjection();
            });
        }
    }
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor.FilterDescriptors.Any(a => a.Filter.GetType() == typeof(EnableQueryAttribute)))
            {
                Dictionary<string, string> odataParameters = new Dictionary<string, string>()
                {
                    { "$expand", "Causes related entities to be included inline in the response"},
                    { "$filter", "A function that must evaluate to true for a record to be returned"},
                    { "$select", "Specifies a subset of properties to return"},
                    { "$orderby", "Determines what values are used to order a collection of records"},
                    { "$top", "The max number of records"},
                    { "$skip", "The number of records to skip"}
                };

                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<IParameter>();
                }

                foreach (var pair in odataParameters)
                {
                    operation.Parameters.Add(new Parameters
                    {
                        Name = pair.Key,
                        Required = false,
                        In="query",
                        Description=pair.Value
                    });
                }
            }

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription
                                         .ParameterDescriptions
                                         .First(p => p.Name == parameter.Name);

                var t = context.ApiDescription.ActionAttributes();

                var routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.Default == null)
                {
                    parameter.Default = routeInfo.DefaultValue;
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}
