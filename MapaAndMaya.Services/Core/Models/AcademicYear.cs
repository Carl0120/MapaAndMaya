namespace MapaAndMaya.Services.Core.Models;

public class AcademicYear : NomenclatureBase
{
    public int Order { get; set; }

    public ICollection<YearsInCourse> YearsInCourse { get; } = new List<YearsInCourse>();
}