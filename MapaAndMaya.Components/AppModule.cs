using EIS.Core.Services.Contracts;
using MapaAndMaya.Services;
using MapaAndMaya.Services.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;

namespace MapaAndMaya.Components;

public class AppModule : IAppModule
{
    public string Name { get; } = "Maya";
    public string Description { get; } = "Modulo de apoyo para gestionar la Maya curricular";
    public string ConfigSettings { get; } = "MapaAndMayaModuleSettings";

    public void AddModule(IServiceCollection services, IConfigurationSection configuration,
        IConfigurationSection globalConfiguration)
    {
        services.AddRadzenComponents();
        var connectionString = configuration.GetSection("ConectionString").Value ??
                               throw new Exception("Connection String Not Found");

        services.AddDbContextFactory<MapaAndMayaDbContext>(optionsAction =>
        {
            optionsAction.UseNpgsql(connectionString,
                builder => { builder.MigrationsAssembly("MapaAndMaya.PostGresSql.Migrations"); }
            );
        }, ServiceLifetime.Scoped);

        //Add Services
        services.AddScoped<DegreeService>();
    }

    public void InitModule(IServiceProvider services)
    {
        var dbContext = services.GetRequiredService<MapaAndMayaDbContext>();
        dbContext.Database.Migrate();
        
    }
}