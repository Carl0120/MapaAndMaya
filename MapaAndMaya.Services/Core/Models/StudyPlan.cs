namespace MapaAndMaya.Services.Core.Models;

public class StudyPlan : NomenclatureBase
{
    public ICollection<Course> Courses { get; } = new List<Course>();
}