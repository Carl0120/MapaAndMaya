
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class TownService : GenericService<Town,GenericViewModel>
{
    public TownService(ILogger<GenericService<Town, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) : base(logger, dbContext)
    {
    }
}
