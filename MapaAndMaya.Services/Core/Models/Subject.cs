namespace MapaAndMaya.Services.Core.Models;

public class Subject : NomenclatureBase
{
    public ICollection<SubjectsInPeriod> SubjectsInPeriods { get; } = new List<SubjectsInPeriod>();
}