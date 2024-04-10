using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MapaAndMaya.Services.DB;

public class MapaAndMayaDbContextFactory : IDesignTimeDbContextFactory<MapaAndMayaDbContext>
{
    public MapaAndMayaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MapaAndMayaDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MapaAndMayaModuleDB;Username=postgres;Password=root;"
            , builder =>
            {
                builder.MigrationsAssembly("MapaAndMaya.PostGresSql.Migrations");
            }
        );
        return new MapaAndMayaDbContext(optionsBuilder.Options);
    }
}