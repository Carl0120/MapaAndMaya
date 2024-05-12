using System.ComponentModel.DataAnnotations.Schema;

namespace MapaAndMaya.Services.Models;

public class Course
{
    public int Id { get; set; }
    
    public bool Universalized { get; set; }
    
    public int YearsNumber { get; set; }
    
    public int ModalityId { get; init; }
    public Modality? Modality { get; init; }
    
    public int DegreeId { get; init; }
    public Degree? Degree { get; init; }

    public ICollection<CourseInCumFum> CourseInCumFum { get; } = new List<CourseInCumFum>();
    
    public ICollection<SubjectInCourse> SubjectInCourses { get; } = new List<SubjectInCourse>();
    
    
}