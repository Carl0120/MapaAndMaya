using MapaAndMaya.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace MapaAndMaya.Services.DB;

public class MapaAndMayaDbContext : DbContext
{
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Degree> Degrees { get; set; }
    
    public MapaAndMayaDbContext(DbContextOptions<MapaAndMayaDbContext> options) : base(options)
    {
    }
}