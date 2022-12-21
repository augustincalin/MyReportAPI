using Microsoft.Extensions.DependencyInjection.Extensions;
using ReportApi.Services.Implementations;
using ReportApi.Services.Interfaces;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;

namespace ReportApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.TryAddSingleton<IDataService, DataService>();
            builder.Services.TryAddSingleton<IMimeMapperService, MimeMapperService>();
            builder.Services.TryAddSingleton<IReportServiceConfiguration>(sp =>
            new ReportServiceConfiguration {
                // The default ReportingEngineConfiguration will be initialized from appsettings.json or appsettings.{EnvironmentName}.json:
                ReportingEngineConfiguration = sp.GetService<IConfiguration>(),
                // In case the ReportingEngineConfiguration needs to be loaded from a specific configuration file, use the approach below:
                //ReportingEngineConfiguration = ResolveSpecificReportingConfiguration(sp.GetService<IWebHostEnvironment>()),
                HostAppId = "ReportingNet6",
                Storage = new FileStorage(),
                ReportSourceResolver = new UriReportSourceResolver(System.IO.Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports"))
            });

            builder.Services.AddControllers().AddNewtonsoftJson();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}