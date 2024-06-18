using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Services.NomenclatureServices;

public class StudyPlanService : GenericService<StudyPlan, GenericViewModel>
{
    public StudyPlanService(ILogger<GenericService<StudyPlan, GenericViewModel>> logger, MapaAndMayaDbContext dbContext)
        : base(logger, dbContext)
    {
    }
}