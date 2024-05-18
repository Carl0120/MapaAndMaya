using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;


public class Degree : NomenclatureBase
{
    private ICollection<DegreeModality> DegreeModalities { get; } = new List<DegreeModality>();
}