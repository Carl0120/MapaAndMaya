namespace MapaAndMaya.Services.Models;

public class YearsInCourse
{
    public int Id { get; init; }
    
    public int CourseId { get; set; }
    public Course Course { get; init; } = null!;

    public int AcademicYearId { get; set; }
    public AcademicYear AcademicYear { get; init; } = null!;

    public ICollection<PeriodInYear> PeriodInYears { get; set; } = new List<PeriodInYear>();
}