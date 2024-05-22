using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class Sede : NomenclatureBase
{
    public int TownId { get; set; }
    public Town? Town { get; set; }

    public int SedeTypeId { get; set; }
    public SedeType? Type { get; set; }

    public ICollection<SedeCourse> SedeCourses { get; } = new List<SedeCourse>();
}