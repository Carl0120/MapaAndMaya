using MapaAndMaya.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace MapaAndMaya.Services.DB;

public class MapaAndMayaDbContext : DbContext
{
    public MapaAndMayaDbContext(DbContextOptions<MapaAndMayaDbContext> options) : base(options)
    {
    }

    //DegreeAndModalities
    public DbSet<Modality> Modalities { get; set; }
    public DbSet<Degree> Degrees { get; set; }
    public DbSet<DegreeModality> DegreeModalities { get; set; }

    //FilialFaculty
    public DbSet<Town> Towns { get; set; }
    public DbSet<SedeType> SedeTypes { get; set; }
    public DbSet<Sede> Sedes { get; set; }

    public DbSet<StudyPlan> StudyPlans { get; set; }
    public DbSet<AcademicCourse> AcademicCourses { get; set; }

    //Subjects
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Period> Periods { get; set; }
    public DbSet<AcademicYear> AcademicYears { get; set; }

    public DbSet<Course> Courses { get; set; }
    public DbSet<SedeCourse> SedeCourses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Degree Modalities
        modelBuilder.Entity<Modality>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<Degree>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<DegreeModality>().HasIndex(e => new { e.ModalityId, e.DegreeId }).IsUnique();

        //Filial Faculty
        modelBuilder.Entity<Town>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<SedeType>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<Sede>().HasIndex(e => e.Name).IsUnique();

        //AcademicCourse StudyPlan
        modelBuilder.Entity<StudyPlan>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<AcademicCourse>().HasIndex(e => e.Name).IsUnique();

        //Subjects
        modelBuilder.Entity<Subject>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<Period>().HasIndex(e => e.Name).IsUnique();

        modelBuilder.Entity<AcademicYear>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<AcademicYear>().HasIndex(e => e.Order).IsUnique();


        //Course
        modelBuilder.Entity<Course>().HasKey(e => e.Id);
        modelBuilder.Entity<Course>().HasOne(e => e.StudyPlan)
            .WithMany(e => e.Courses).HasForeignKey(e => e.StudyPlanId).IsRequired();
        modelBuilder.Entity<Course>().HasOne(e => e.AcademicCourse)
            .WithMany(e => e.Courses).HasForeignKey(e => e.AcademicCourseId).IsRequired();
        modelBuilder.Entity<Course>().HasOne(e => e.DegreeModality)
            .WithMany(e => e.Courses).HasForeignKey(e => e.DegreeModalityId).IsRequired();
        modelBuilder.Entity<Course>().HasIndex(e => new { e.DegreeModalityId, e.StudyPlanId, e.AcademicCourseId })
            .IsUnique();


        base.OnModelCreating(modelBuilder);
    }
}