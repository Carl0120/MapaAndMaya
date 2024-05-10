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
    public DbSet<CourseInCumFum> CourseInCumFums { get; set; }
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
        
        //Entity CourseInCumFum
        modelBuilder.Entity<CourseInCumFum>().HasKey(e=>e.Id);
        modelBuilder.Entity<CourseInCumFum>().HasIndex(e=> new {e.CumFumId, e.CourseId}).IsUnique();
        modelBuilder.Entity<CourseInCumFum>().HasOne(e => e.CumFum)
            .WithMany(e => e.CourseInCumFum);
        modelBuilder.Entity<CourseInCumFum>().HasOne(e => e.Course)
            .WithMany(e => e.CourseInCumFum);
        
        //Entity Group
        modelBuilder.Entity<Group>().HasIndex(e => new {e.AcademicYear,e.CourseInCumFumId}).IsUnique();
        modelBuilder.Entity<Group>().HasKey(e => e.Id);
        base.OnModelCreating(modelBuilder);
    }
}