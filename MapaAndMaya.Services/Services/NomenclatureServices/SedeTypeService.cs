using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Services.NomenclatureServices;

public class SedeTypeService : GenericService<SedeType, GenericViewModel>
{
    public SedeTypeService(ILogger<GenericService<SedeType, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) :
        base(logger, dbContext)
    {
    }
}