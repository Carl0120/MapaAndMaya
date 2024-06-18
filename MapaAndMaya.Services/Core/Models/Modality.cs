namespace MapaAndMaya.Services.Core.Models;

public class Modality : NomenclatureBase
{
    private ICollection<DegreeModality> DegreeModalities { get; } = new List<DegreeModality>();
}