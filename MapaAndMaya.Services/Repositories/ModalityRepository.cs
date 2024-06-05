using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Repositories;

public class ModalityRepository : GenericRepository<Modality, GenericViewModel>
{
    public ModalityRepository(ILogger<GenericRepository<Modality, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) : base(logger, dbContext)
    {
    }
    
    
}