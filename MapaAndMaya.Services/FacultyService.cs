using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class FacultyService
{
    private readonly ILogger<FacultyService> _logger;

    public readonly MapaAndMayaDbContext _dbContext;

    public FacultyService(ILogger<FacultyService> logger, MapaAndMayaDbContext dbContext)
    {
        this._logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ActionResult<Faculty>> Create(FacultyViewModel model)
    {
        var anyTask = _dbContext.Faculties.AnyAsync(p => p.Name == model.Name);
        
        ActionResult<Faculty> result = new ActionResult<Faculty>();
        
        if (await anyTask)
        {
            result.Errors.Add("Ya existe una facultad con ese nombre");
        }
        if (result.Errors.Any())
        {
            result.Status = false;
            result.Severity = NotifySeverity.Warning;
            result.Title = "Accion Inválida";
            return result;
        }

        Faculty entity = new Faculty();
        model.CopyToEntity(entity);
        
        try
        {
            EntityEntry<Faculty> resp = _dbContext.Faculties.Add(entity);
            await _dbContext.SaveChangesAsync();
            result.Title = "Exito";
            result.Severity = NotifySeverity.Succes;
            result.Status = true;
            result.Result = resp.Entity;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.Title = "Fallo";
            result.Severity = NotifySeverity.Error;
            result.Status = false;
            result.Errors.Add((ex.Message));
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
        }
        
        if (result.Errors.Any())
        {
            result.Status = false;
            result.Severity = NotifySeverity.Warning;
            result.Title = "Accion Inválida";
            return result;
        }
      
        model.CopyToEntity(entity!);
        try
        {
            _dbContext.Faculties.Update(entity!);
            await _dbContext.SaveChangesAsync();
            result.Title = "Exito";
            result.Severity = NotifySeverity.Succes;
            result.Status = true;
            result.Result = entity;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.Title = "Fallo";
            result.Severity = NotifySeverity.Error;
            result.Status = false;
            result.Errors.Add(ex.Message);
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
            result.Status = true;
            result.Title = "Exito";
            result.Severity = NotifySeverity.Succes;
            result.Result = entity;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.Title = "Fallo";
            result.Severity = NotifySeverity.Error;
            result.Status = false;
            result.Errors.Add(ex.Message);
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