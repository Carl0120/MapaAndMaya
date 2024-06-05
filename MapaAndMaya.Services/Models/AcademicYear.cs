using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class AcademicYear : NomenclatureBase
{
    public int Order { get; set; }
    
    public ICollection<YearsInCourse> YearsInCourse { get; } = new List<YearsInCourse>();
}