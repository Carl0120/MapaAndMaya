namespace MapaAndMaya.Services.Models;

public class SedeCourse
{
    public int Id { get; set;}
    
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    
    public int SedeId { get; set; }
    public Sede? Sede { get; set; }
    
}