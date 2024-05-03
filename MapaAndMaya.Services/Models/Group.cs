namespace MapaAndMaya.Services.Models;

public class Group
{
    public int Id { get; set;}
    
    public int AcademicYear { get; set;}
    
    public int Enrollment { get; set; }
    
    public int CumFumId { get; set; }

    public int CourseId { get; set; }

    public CumFum? CumFum { get; set;}
    
    public Course? Course { get; set;}
}