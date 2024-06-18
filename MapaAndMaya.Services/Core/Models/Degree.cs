namespace MapaAndMaya.Services.Core.Models;

public class Degree : NomenclatureBase
{
    public ICollection<DegreeModality> DegreeModalities { get; } = new List<DegreeModality>();
}