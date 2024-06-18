namespace MapaAndMaya.Services.Core.Models;

public class Period : NomenclatureBase
{
    public ICollection<PeriodInYear> PeriodInYears { get; } = new List<PeriodInYear>();
}