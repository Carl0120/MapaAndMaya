using MapaAndMaya.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace MapaAndMaya.Services.DB;

public class MapaAndMayaDbContext : DbContext
{
    public MapaAndMayaDbContext(DbContextOptions<MapaAndMayaDbContext> options) : base(options)
    {
    }

    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Degree> Degrees { get; set; }
    public DbSet<Modality> Modalities { get; set;}
    public DbSet<Course>  Courses { get; set;}
    public DbSet<Town> Towns { get; set; }
    public DbSet<CumFum> CumFums { get; set; }
    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity Faculty
        modelBuilder.Entity<Faculty>().Property(e => e.Name).HasMaxLength(50);
        modelBuilder.Entity<Faculty>().HasIndex(e =>e.Name).IsUnique();
        
        // Entity Degree
        modelBuilder.Entity<Degree>().Property(e => e.Name).HasMaxLength(50);
        modelBuilder.Entity<Degree>().HasIndex(e =>e.Name).IsUnique();
        
        // Entity Modality
        modelBuilder.Entity<Modality>().HasKey(e =>e.Id);
        modelBuilder.Entity<Modality>().Property(e => e.Name).HasMaxLength(50);
        
        // Entity Town
        modelBuilder.Entity<Town>().Property(e => e.Name).HasMaxLength(50);
        modelBuilder.Entity<Town>().HasIndex(e =>e.Name).IsUnique();
        modelBuilder.Entity<Town>().HasOne(e => e.CumFum)
            .WithOne(e => e.Town).HasForeignKey<CumFum>(e => e.Id);
        
        //Entity Course
        modelBuilder.Entity<Course>().HasKey(e => e.Id);
        modelBuilder.Entity<Course>().HasIndex(e=> new{e.DegreeId,e.ModalityId}).IsUnique();
        
        //Entity CumFum
        modelBuilder.Entity<CumFum>().Property(e => e.Name).HasMaxLength(50);
        modelBuilder.Entity<CumFum>().HasIndex(e =>e.Name).IsUnique();
        
        //Entity Group
        modelBuilder.Entity<Group>().HasKey(e=>e.Id);
        modelBuilder.Entity<Group>().HasIndex(e=> new {e.CumFumId, e.CourseId,e.AcademicYear}).IsUnique();
        modelBuilder.Entity<Group>().HasOne(e => e.CumFum)
            .WithMany(e => e.Groups);
        modelBuilder.Entity<Group>().HasOne(e => e.Course)
            .WithMany(e => e.Groups);
        
        base.OnModelCreating(modelBuilder);
    }
}