namespace MapaAndMaya.Services.Models;

public class Degree : NomenclatureBase
{
    public ICollection<DegreeModality> DegreeModalities { get; } = new List<DegreeModality>();
}