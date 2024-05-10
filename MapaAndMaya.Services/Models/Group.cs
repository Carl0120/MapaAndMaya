namespace MapaAndMaya.Services.Models;

public class Group
{
    public int Id { get; set;}
    
    public int AcademicCourse { get; set; }
    
    public int AcademicYear { get; set;}
    
    public int Enrollment { get; set; }

    public int CourseInCumFumId { get; set; }

    public CourseInCumFum?  CourseInCumFum { get; set; }
}