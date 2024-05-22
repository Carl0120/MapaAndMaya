using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class Town : NomenclatureBase
{
    public ICollection<Sede> FacultyFilials { get; } = new List<Sede>();
}