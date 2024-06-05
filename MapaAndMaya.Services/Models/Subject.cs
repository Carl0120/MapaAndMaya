using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class Subject : NomenclatureBase
{
    public ICollection<SubjectsInPeriod> SubjectsInPeriods { get; } = new List<SubjectsInPeriod>();
}