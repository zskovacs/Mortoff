using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Mortoff.Application.Common.Exceptions;
using Mortoff.Common;
using Mortoff.Persistence;
using Mortoff.WebUI.Filters;
using Mortoff.WebUI.Middlewares;
using Newtonsoft.Json;

namespace Mortoff.WebUI;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.InstallServicesInAssembly(Configuration);

        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();

        services.AddHttpContextAccessor();

        services.AddControllersWithViews(config =>
        {
            config.Filters.Add<OperationCancelledExceptionFilter>();
        }).AddNewtonsoftJson(
            options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });

        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddSpaStaticFiles(configuration =>
            configuration.RootPath = "ClientApp/dist");

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "Mortoff API";
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseCustomExceptionHandler();
        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        if (!env.IsDevelopment())
            app.UseSpaStaticFiles();

        app.UseOpenApi();

        app.UseSwaggerUi3(settings =>
        {
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
            endpoints.MapControllers();
        });

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
                spa.UseProxyToSpaDevelopmentServer(Configuration["SpaBaseUrl"] ?? "http://localhost:4200");
            }
        });
    }
}
