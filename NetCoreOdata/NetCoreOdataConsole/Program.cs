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
            using (DBContext db =new DBContext("server=192.168.8.144;database=ogc;user=root;password=root;SslMode=none"))
            {
                object industrial_Area = db.IndustrialArea.AsEnumerable();
                object thing = db.Thing.AsEnumerable();

            };
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
            services.AddDbContext<DBContext>(options => options.UseMySql(_configuration["MysqlConnectionString"]));
            services.AddOData();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Title = "Sensor Thing Odata API",
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
            builder.EntitySet<Featureofinterest>(nameof(Featureofinterest));
            builder.EntitySet<Historicallocation>(nameof(Historicallocation));
            builder.EntitySet<Observedproperty>(nameof(Observedproperty));
            builder.EntitySet<Observation>(nameof(Observation));
            builder.EntitySet<Location>(nameof(Location));

            app.UseMvc(route =>
            {
                route.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
                route.MapODataServiceRoute("OdataRoute", "Odata", builder.GetEdmModel());
                route.EnableDependencyInjection();
            });
        }
    }
}
