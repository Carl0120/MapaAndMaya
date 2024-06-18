namespace MapaAndMaya.Services.Core.Models;

public class SedeType : NomenclatureBase
{
    public ICollection<Sede> FacultyFilials { get; } = new List<Sede>();
}