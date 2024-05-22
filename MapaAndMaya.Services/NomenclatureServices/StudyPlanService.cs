using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.NomenclatureServices;

public class StudyPlanService : GenericService<StudyPlan, GenericViewModel>
{
    public StudyPlanService(ILogger<GenericService<StudyPlan, GenericViewModel>> logger, MapaAndMayaDbContext dbContext)
        : base(logger, dbContext)
    {
    }
}