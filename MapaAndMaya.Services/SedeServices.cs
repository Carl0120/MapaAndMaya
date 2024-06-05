using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class SedeService :ICrudService<Sede,SedeViewModel>
{
    private readonly ILogger<SedeService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;


    public SedeService(ILogger<SedeService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<ActionResult<Sede>> Create(SedeViewModel model)
    {
        ActionResult<Sede> result = new();
        var existAny = await _dbContext.Sedes.AnyAsync(p => p.Name == model.Name || p.Id == model.Id);
        
        if (existAny) result.Errors.Add("El elemento ya existe");
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        var entity = new Sede();
        model.ToEntity(entity);
        try
        {
            EntityEntry<Sede> entityEntry = _dbContext.Sedes.Add(entity);
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

    public async Task<ActionResult<Sede>> Update(SedeViewModel model)
    {
        ActionResult<Sede> result = new();

        var entity = await _dbContext.Sedes.FirstOrDefaultAsync(e => e.Id == model.Id);
        var existAny = await _dbContext.Sedes.AnyAsync(p => p.Name == model.Name && p.Id != model.Id);
        
        if (entity == null) result.Errors.Add("No se encuentra el elemento");
        if (existAny) result.Errors.Add("El nombre ya esta en uso ");

        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        model.ToEntity(entity!);
        try
        {
            _dbContext.Sedes.Update(entity!);
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

    public async Task<ActionResult<IList<Sede>>> Delete(IList<Sede> entities)
    {
        ActionResult<IList<Sede>> result = new();
        
        var ids = entities.Select(e => e.Id).ToList();
        try
        {
            await _dbContext.AcademicYears.Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync();
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

    public IEnumerable<Sede> Find()
    {
        try
        {
            var list = _dbContext.Sedes
                .OrderBy(e => e.Name)
                .Include(e=>e.Town)
                .Include(e=>e.Type)
                .AsEnumerable();
            return list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Enumerable.Empty<Sede>();
        }
    }
}