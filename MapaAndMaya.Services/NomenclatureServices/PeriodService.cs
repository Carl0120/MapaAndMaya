using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.NomenclatureServices;

public class PeriodService : GenericService<Period, GenericViewModel>
{
    public PeriodService(ILogger<GenericService<Period, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) :
        base(logger, dbContext)
    {
    }
}