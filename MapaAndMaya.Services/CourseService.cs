using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class CourseService : ICrudService<Course,CourseViewModel>
{
    private readonly ILogger<CourseService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;


    public CourseService(ILogger<CourseService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ActionResult<Course>> Create(CourseViewModel model)
    {
        ActionResult<Course> result = new();
        var existAny = await _dbContext.Courses.AnyAsync(p =>p.Id == model.Id);
        var existAny2 = await _dbContext.Courses.AnyAsync(p =>p.AcademicCourseId == model.AcademicCourseId && p.StudyPlanId==model.StudyPlanId&& p.DegreeModalityId==model.DegreeModalityId);
        
        if (existAny || existAny2) result.Errors.Add("El Curso ya existe");
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        var entity = new Course();
        model.ToEntity(entity);
        try
        {
            EntityEntry<Course> entityEntry = _dbContext.Courses.Add(entity);
            await _dbContext.SaveChangesAsync();
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

    public async Task<ActionResult<Course>> Update(CourseViewModel model)
    {
        ActionResult<Course> result = new();

        var entity = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == model.Id);
        
        if (entity == null) result.Errors.Add("No se encuentra el elemento");
        
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        model.ToEntity(entity!);
        try
        {
            _dbContext.Courses.Update(entity!);
            await _dbContext.SaveChangesAsync();
            result.CreateResponseSuccess(entity!);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.CreateResponseFail(ex);
            return result;
        }
    }

    public async Task<ActionResult<IList<Course>>> Delete(IList<Course> entities)
    {
        ActionResult<IList<Course>> result = new();
        
        var ids = entities.Select(e => e.Id).ToList();
        try
        {
            await _dbContext.Courses.Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync();
            result.CreateResponseSuccess(entities);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.CreateResponseFail(ex);
            return result;
        }
    }

    public IEnumerable<Course> Find()
    {
        try
        {
            var list = _dbContext.Courses
                .OrderByDescending(e=>e.AcademicCourse.Name)
                .Include(e=>e.AcademicCourse)
                .Include(e=>e.StudyPlan)
                .Include(e=>e.DegreeModality)
                .ThenInclude(e=>e.Degree)
                .Include(e=>e.DegreeModality)
                .ThenInclude(e=>e.Modality)
                .Include(e=>e.YearsInCourse)
                .AsEnumerable();
            return list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Enumerable.Empty<Course>();
        }
    }
    
    public Course? Find(int courseId)
    {
        try
        {
            var course = _dbContext.Courses
                .Include(e=>e.AcademicCourse)
                .Include(e=>e.StudyPlan)
                .Include(e=>e.DegreeModality)
                .ThenInclude(e=>e.Degree)
                .Include(e=>e.DegreeModality)
                .ThenInclude(e=>e.Modality)
                .FirstOrDefault(e=>e.Id==courseId);
            return course;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}