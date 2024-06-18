using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Services;

public class SedeAssignmentService
{
    private readonly MapaAndMayaDbContext _dbContext;
    private readonly ILogger<SedeAssignmentService> _logger;

    public SedeAssignmentService(ILogger<SedeAssignmentService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ActionResult<List<SedeCourse>>> Create(SedeAssignmentViewModel model)
    {
        ActionResult<List<SedeCourse>> result = new();
        var existList = await _dbContext.SedeCourses
            .Where(p => p.CourseId == model.CourseId && model.SedeIdList.Contains(p.SedeId))
            .Select(e => e.SedeId)
            .ToListAsync();

        if (existList.Count >= model.SedeIdList.Count)
        {
            result.Errors.Add("Las curso ya exta asignado a las Cedes seleccionadas");
            result.CreateResponseInvalidAction();
            return result;
        }

        foreach (var item in existList) model.SedeIdList.Remove(item);

        var entities = new List<SedeCourse>();
        model.ToEntity(entities);

        try
        {
            await _dbContext.SedeCourses.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
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

    public async Task<ActionResult<IList<SedeCourse>>> Delete(IList<SedeCourse> entities)
    {
        ActionResult<IList<SedeCourse>> result = new();

        var ids = entities.Select(e => e.Id).ToList();
        try
        {
            await _dbContext.SedeCourses.Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync();
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

    public async Task<ICollection<SedeCourse>> GetSedeInCurse(int courseId)
    {
        try
        {
            var list = _dbContext.SedeCourses
                .OrderBy(e => e.Sede.Name)
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Sede)
                .ThenInclude(e => e.Town)
                .Include(e => e.Sede)
                .ThenInclude(e => e.Type)
                .ToListAsync();
            return await list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<SedeCourse>();
        }
    }
}