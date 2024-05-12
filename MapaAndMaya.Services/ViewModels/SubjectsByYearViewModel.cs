using MapaAndMaya.Services.Models;

namespace MapaAndMaya.Services.ViewModels;

public class SubjectsByYearViewModel
{
    public int Year { get; set; }
    
    public ICollection<SubjectInCourse> SubjectsOfFirstPeriod { get; init; } = new List<SubjectInCourse>();
    
    public ICollection<SubjectInCourse> SubjectsOfSecondPeriod { get; init; } = new List<SubjectInCourse>();
    
}