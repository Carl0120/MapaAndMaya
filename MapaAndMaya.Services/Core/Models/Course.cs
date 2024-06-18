namespace MapaAndMaya.Services.Core.Models;

public class Course
{
    public int Id { get; init; }

    public int DegreeModalityId { get; set; }
    public DegreeModality DegreeModality { get; set; } = null!;

    public int AcademicCourseId { get; set; }
    public AcademicCourse AcademicCourse { get; set; } = null!;

    public int StudyPlanId { get; set; }
    public StudyPlan StudyPlan { get; set; } = null!;

    public ICollection<SedeCourse> SedeCourses { get; } = new List<SedeCourse>();

    public ICollection<YearsInCourse> YearsInCourse { get; } = new List<YearsInCourse>();

    public int NumberOfYears => YearsInCourse.Count;
}