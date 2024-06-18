using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Services.NomenclatureServices;

public class TownService : GenericService<Town, GenericViewModel>
{
    public TownService(ILogger<GenericService<Town, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) : base(
        logger, dbContext)
    {
    }
}