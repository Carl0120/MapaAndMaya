using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Services.NomenclatureServices;

public class SubjectService : GenericService<Subject, GenericViewModel>
{
    public SubjectService(ILogger<GenericService<Subject, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) :
        base(logger, dbContext)
    {
    }
}