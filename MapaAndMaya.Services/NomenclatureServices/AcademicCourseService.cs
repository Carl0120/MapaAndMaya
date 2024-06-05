using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.Repositories;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.NomenclatureServices;

public class AcademicCourseService : GenericService<AcademicCourse, GenericViewModel>
{
    public AcademicCourseService(ILogger<GenericService<AcademicCourse, GenericViewModel>> logger,
        MapaAndMayaDbContext dbContext) : base(logger, dbContext)
    {
    }
}