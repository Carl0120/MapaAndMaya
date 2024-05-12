using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class GroupService
{
    private readonly ILogger<GroupService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public GroupService(ILogger<GroupService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }


    public async Task<ActionResult<Group>> Create(GroupToCourseRequest model)
    {
        var result = new ActionResult<Group>();
        var entity = new Group();

        var course = _dbContext.CourseInCumFums
            .Include(e => e.Course)
            .FirstOrDefault(e => e.Id == model.CourseInCumFumId);

        if (course == null)
        {
            result.Errors.Add("El curso solicitado no existe en este Cum-Fum");
            result.CreateResponseInvalidAction();
            return result;
        }

        if (course.Course!.YearsNumber < model.AcademicYear)
            result.Errors.Add("El año académico exede la duración del curso");

        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        model.CopyToEntity(entity);

        try
        {
            var resp = _dbContext.Groups.Add(entity);
            await _dbContext.SaveChangesAsync();
            result.CreateResponseSuccess(resp.Entity);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.CreateResponseFail(ex);
            return result;
        }
    }

    public async Task<ActionResult<Group>> Update(GroupToCourseRequest model)
    {
        var result = new ActionResult<Group>();

        var entity = await _dbContext.Groups
            .Include(e => e.CourseInCumFum)
            .ThenInclude(courseInCumFum => courseInCumFum!.Course!)
            .FirstOrDefaultAsync(e => e.Id == model.Id);

        if (entity == null)
        {
            result.Errors.Add("El grupo no existe");
            result.CreateResponseInvalidAction();
            return result;
        }

        if (entity.CourseInCumFum!.Course!.YearsNumber < model.AcademicYear)
            result.Errors.Add("El año académico exede la duración del curso");

        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        model.CopyToEntity(entity);

        try
        {
            _dbContext.Groups.Update(entity);
            await _dbContext.SaveChangesAsync();
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

    public async Task<ActionResult<Group>> Delete(Group entity)
    {
        var result = new ActionResult<Group>();
        try
        {
            var item = await _dbContext.Groups.FindAsync(entity.Id);
            if (item != null)
            {
                _dbContext.Groups.Remove(item);
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