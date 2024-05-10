using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class CourseOfCumFumService
{
    
    private readonly ILogger<CourseOfCumFumService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public CourseOfCumFumService(ILogger<CourseOfCumFumService> logger, MapaAndMayaDbContext dbContext)
    {
        this._logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<ActionResult<CourseInCumFum>> AddCourseToCumFum(AddCourseToCumFumRequest model)
    {
        ActionResult<CourseInCumFum> result = new ActionResult<CourseInCumFum>();
        
        if(_dbContext.CourseInCumFums.Any(e=>e.CumFumId == model.CumFumId && e.CourseId == model.CourseId))
            result.Errors.Add("El curso ya existe en este CUM-FUM");
        
        
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }
        
        try
        {
            EntityEntry<CourseInCumFum> resp = _dbContext.CourseInCumFums.Add(new CourseInCumFum(){CumFumId = model.CumFumId, CourseId = model.CourseId});
            await _dbContext.SaveChangesAsync();
            
            CourseInCumFum course = await GetCourseById(resp.Entity.Id);
            result.CreateResponseSuccess(course);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.CreateResponseFail(ex);
            return result;
        }
    }
   
    public async Task<ICollection<CourseInCumFum>> GetCourseByCumFum(int cumFumId)
    {
        try
        {
            var query = _dbContext.CourseInCumFums
                .Where(course => course.CumFumId == cumFumId)
                .Include(e => e.Course)
                .ThenInclude(e => e!.Degree)
                .ThenInclude(e => e!.Faculty)
                .Include(e => e.Course)
                .ThenInclude(e => e!.Modality)
                .Include(e => e.Groups)
                .AsNoTracking();
            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<CourseInCumFum>();
        }
    }

    public async Task<ICollection<Degree>> GetDegreeWithCourses()
    {    
        try
        {
            var query = _dbContext.Degrees
                .Where(degree => degree.Courses.Any())
                .Include(e => e.Courses)
                .ThenInclude(e=>e.Modality);
            
        
        return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<Degree>();
        }
    }
 
    private async Task<CourseInCumFum> GetCourseById(int courseId)
    {
            var query = _dbContext.CourseInCumFums
                .Include(e => e.Course)
                .ThenInclude(e => e!.Degree)
                .ThenInclude(e => e!.Faculty)
                .Include(e => e.Course)
                .ThenInclude(e => e!.Modality)
                .Include(e => e.Groups)
                .AsNoTracking();
            return await query.FirstAsync(e=>e.Id == courseId);
        
    }
    
    public async Task<ActionResult<CourseInCumFum>> Delete(CourseInCumFum entity)
    {
        ActionResult<CourseInCumFum> result = new ActionResult<CourseInCumFum>();
        
        try
        {
            CourseInCumFum? item = await _dbContext.CourseInCumFums.FindAsync(entity.Id);
            if (item != null)
            {
                _dbContext.CourseInCumFums.Remove(item);
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
    
    
}