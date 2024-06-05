using MapaAndMaya.Services.Models;

namespace MapaAndMaya.Services.ViewModels;

public class CourseViewModel
{
    public int Id { get; init; }
    
    public int DegreeId { get; set; }
    
    
    public int DegreeModalityId { get; set; }
    
    public int AcademicCourseId { get; set; }
    
    public int StudyPlanId { get; set; }

    public static CourseViewModel Clone(Course entity)
    {
        return new CourseViewModel
        {
            Id = entity.Id,
            DegreeId = entity.DegreeModality.DegreeId,
            AcademicCourseId = entity.AcademicCourseId,
            DegreeModalityId = entity.DegreeModalityId,
            StudyPlanId = entity.StudyPlanId,
        };
    }
}