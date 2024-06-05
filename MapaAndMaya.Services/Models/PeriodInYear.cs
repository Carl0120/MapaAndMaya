namespace MapaAndMaya.Services.Models;

public class PeriodInYear
{
   

    public int Id { get; set; }
    
    public int YearsInCourseId { get; set; }
    public YearsInCourse? YearsInCourse { get; set; }
    
     public int PeriodId { get; set; }
     public  Period Period { get; set; } = null!;

     public ICollection<SubjectsInPeriod> SubjectsInPeriods { get; } = new List<SubjectsInPeriod>();
}