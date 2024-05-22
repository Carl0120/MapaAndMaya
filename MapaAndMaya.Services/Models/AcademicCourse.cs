using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class AcademicCourse : NomenclatureBase
{
    public int Order { get; set; }

    public ICollection<Course> Courses { get; } = new List<Course>();
}