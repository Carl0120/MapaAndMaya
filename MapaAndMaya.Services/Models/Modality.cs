using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class Modality : NomenclatureBase
{
    private ICollection<DegreeModality> DegreeModalities { get; } = new List<DegreeModality>();
}