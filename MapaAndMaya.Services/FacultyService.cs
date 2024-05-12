using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class FacultyService : ICrudService<Faculty,FacultyViewModel>
{
    private readonly ILogger<FacultyService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public FacultyService(ILogger<FacultyService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ActionResult<Faculty>> Create(FacultyViewModel model)
    {
        var anyTask = _dbContext.Faculties.AnyAsync(p => p.Name == model.Name);
        
        ActionResult<Faculty> result = new ActionResult<Faculty>();
        
        if (await anyTask) result.Errors.Add("Ya existe una facultad con ese nombre");
        
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        Faculty entity = new Faculty();
        model.CopyToEntity(entity);
        
        try
        {
            EntityEntry<Faculty> resp = _dbContext.Faculties.Add(entity);
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
    
    public async Task<ActionResult<Faculty>> Update(FacultyViewModel model)
    {
        var findTask = _dbContext.Faculties.FindAsync(model.Id);
        
        ActionResult<Faculty> result = new ActionResult<Faculty>();
        
        Faculty? entity = await findTask;
        
        if (entity == null)
        {
            result.Errors.Add("No se encuentra la facultad");
            result.CreateResponseInvalidAction();
            return result;
        }
        
        model.CopyToEntity(entity);
        try
        {
            _dbContext.Faculties.Update(entity);
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

    public async Task<ActionResult<Faculty>> Delete(Faculty entity)
    {
        ActionResult<Faculty> result = new ActionResult<Faculty>();
        
        try
        {
            Faculty? item = await _dbContext.Faculties.FindAsync(entity.Id);
            if (item != null)
            {
                _dbContext.Faculties.Remove(item);
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
    
    public async Task<ICollection<Faculty>> FindAll()
    {
        try
        {
            var listTask =  _dbContext.Faculties.AsNoTracking().ToListAsync();
            
            return await listTask;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<Faculty>();
        }
    }

    public async Task<Faculty?> Find(int id)
    {
        try
        {
            var itemTask = _dbContext.Faculties
                 .FirstOrDefaultAsync(e => e.Id == id);
            return await itemTask;

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    
}