namespace MapaAndMaya.Services.Models;

public class Subject
{
    public int Id {get; init; }

    public string Name { get; set; } = "";
    
    public ICollection<SubjectInCourse> SubjectInCourses { get; } = new List<SubjectInCourse>();
}