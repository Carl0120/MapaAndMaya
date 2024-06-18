using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Services.NomenclatureServices;

public class AcademicCourseService : GenericService<AcademicCourse, GenericViewModel>
{
    public AcademicCourseService(ILogger<GenericService<AcademicCourse, GenericViewModel>> logger,
        MapaAndMayaDbContext dbContext) : base(logger, dbContext)
    {
    }
}