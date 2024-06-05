namespace MapaAndMaya.Services.Models;

public class SubjectsInPeriod
{
    public int Id { get; set; }
    
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
    
    public bool HaveFinalExam { get; set; }
    
    public int PeriodInYearId { get; set; }
    public PeriodInYear PeriodInYear { get; set; } = null!;
}