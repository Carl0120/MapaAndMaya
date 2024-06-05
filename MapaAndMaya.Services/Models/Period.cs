namespace MapaAndMaya.Services.Models;

public class Period : NomenclatureBase
{
    public ICollection<PeriodInYear> PeriodInYears { get; } = new List<PeriodInYear>();
}