namespace MapaAndMaya.Services.Models;

public class Course
{
    public int Id { get; set; }
    
    public int DegreeModalityId { get; set; }
    public DegreeModality? DegreeModality { get; set;}
    
    public int AcademicCourseId { get; set; }
    public AcademicCourse? AcademicCourse { get; set;}
    
    public int StudyPlanId { get; set; }
    public StudyPlan? StudyPlan { get; set; }

    public ICollection<SedeCourse> SedeCourses { get; } = new List<SedeCourse>();
}