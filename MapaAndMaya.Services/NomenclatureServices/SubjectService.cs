using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.NomenclatureServices;

public class SubjectService : GenericService<Subject, GenericViewModel>
{
    public SubjectService(ILogger<GenericService<Subject, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) :
        base(logger, dbContext)
    {
    }
}