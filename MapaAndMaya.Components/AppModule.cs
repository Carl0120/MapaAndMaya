using EIS.Core.Services.Contracts;
using MapaAndMaya.Services;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.Services;
using MapaAndMaya.Services.Services.NomenclatureServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        },ServiceLifetime.Transient);

    AddServices(services);
    AddRepositories(services);
    }

    public void InitModule(IServiceProvider services)
    {
        var dbContext = services.GetRequiredService<MapaAndMayaDbContext>();
        dbContext.Database.Migrate();
    }

    private void AddServices(IServiceCollection services)
    {
        services.AddTransient<DegreeService>();
        services.AddTransient<ModalityService>();
        services.AddTransient<TownService>();
        services.AddTransient<SedeTypeService>();
        services.AddTransient<SubjectService>();
        services.AddTransient<AcademicCourseService>();
        services.AddTransient<AcademicYearService>();
        services.AddTransient<PeriodService>();
        services.AddTransient<StudyPlanService>();
        services.AddTransient<SedeService>();
        services.AddTransient<CourseService>();
        services.AddTransient<YearsAssignmentService>();
        services.AddTransient<SubjectsAssignmentService>();
        services.AddTransient<SedeAssignmentService>();
    }
    private void AddRepositories(IServiceCollection services)
    {
      
       
    }
    
    
    
}