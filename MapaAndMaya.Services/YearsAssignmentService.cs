using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class YearsAssignmentService
{
    private readonly ILogger<YearsAssignmentService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;


     public YearsAssignmentService(ILogger<YearsAssignmentService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
     public async Task<ActionResult<YearsInCourse>> Create(YearsAssignmentViewModel model)
    {
        ActionResult<YearsInCourse> result = new();
        var existAny = await _dbContext.YearsInCourses.AnyAsync(p =>p.AcademicYearId == model.AcademicYearId && p.CourseId==model.CourseId);
        
        if (existAny) result.Errors.Add("El Año Seleccionado ya ha sido Asignado al Curso");
        
        foreach (var periodsId in model.PeriodsId)
        {
            var existPeriod = await _dbContext.Periods.AnyAsync(p =>p.Id == periodsId);
            if (!existPeriod)
            {
                result.Errors.Add("");
            }
        }
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        var entity = new YearsInCourse();
       model.ToEntity(entity);
       
            try
            {
                EntityEntry<YearsInCourse> entityEntry = _dbContext.YearsInCourses.Add(entity);
                await _dbContext.SaveChangesAsync();
                
                await entityEntry.Reference(e => e.AcademicYear).LoadAsync();
                result.CreateResponseSuccess(entityEntry.Entity);
                return result;
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex.Message);
                result.CreateResponseFail(ex);

                return result;
            }
        
    }
     
     public async Task<ActionResult<YearsInCourse>> Delete(YearsInCourse entity)
        {
            ActionResult<YearsInCourse> result = new();
            
            try
            {
                YearsInCourse? element =  await _dbContext.YearsInCourses.FindAsync(entity.Id);
                if (element != null)
                {
                    _dbContext.YearsInCourses.Remove(element);
                    await _dbContext.SaveChangesAsync();
                }
                result.CreateResponseSuccess(entity);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result.CreateResponseFail(ex);
                return result;
            }
        }
        
     public async Task<List<AcademicYear>> GetWhitYearsInCourse(int courseId)
        {
            try
            {
                var list = _dbContext.AcademicYears
                    .OrderBy(e => e.Order).AsNoTracking().ToListAsync();
                return await list;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<AcademicYear>();
            }
        }

     public async Task<ICollection<YearsInCourse>> GetYearsInCourse(int courseId)
        {
            try
            {
                var list = _dbContext.YearsInCourses
                    .OrderBy(e => e.AcademicYear.Order)
                    .Where(e=>e.CourseId== courseId)
                    .Include(e=>e.AcademicYear)
                    .Include(e=>e.PeriodInYears)
                    .ToListAsync();
                return await list;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<YearsInCourse>();
            }
        }
     
     
}