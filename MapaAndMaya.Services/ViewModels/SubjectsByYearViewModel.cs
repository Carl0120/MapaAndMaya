using MapaAndMaya.Services.Models;

namespace MapaAndMaya.Services.ViewModels;

public class SubjectsByYearViewModel
{
    public int Year { get; set; }
    
    public ICollection<SubjectInCourse> SubjectsOfFirstPeriod { get; set; } = new List<SubjectInCourse>();
    
    public ICollection<SubjectInCourse> SubjectsOfSecondPeriod { get; set; } = new List<SubjectInCourse>();
    
}