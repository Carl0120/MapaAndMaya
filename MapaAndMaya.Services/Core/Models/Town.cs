namespace MapaAndMaya.Services.Core.Models;

public class Town : NomenclatureBase
{
    public ICollection<Sede> FacultyFilials { get; } = new List<Sede>();
}