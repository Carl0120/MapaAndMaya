namespace MapaAndMaya.Services.Models;

public class SubjectInCourse
{
    public int Id { get; set; }
    
    public int Year { get; set; }
    
    public bool Period { get; set; }
    
    public bool HaveFinalExam { get; set; }
    
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
    
}