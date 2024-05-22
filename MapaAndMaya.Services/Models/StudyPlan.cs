using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class StudyPlan : NomenclatureBase
{
    public ICollection<Course> Courses { get; } = new List<Course>();
}