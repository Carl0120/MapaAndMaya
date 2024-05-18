using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public abstract class GenericService<TE,TM> : ICrudService<TE,TM> where TE : NomenclatureBase, new() where TM: GenericViewModel
{
    private readonly ILogger<GenericService<TE,TM>> _logger;

    private readonly MapaAndMayaDbContext _dbContext;
    
    private readonly DbSet<TE> _dbSet;
    
    public GenericService(ILogger<GenericService<TE,TM>> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TE>();
    }

    public async Task<ActionResult<TE>> Create(TM model)
    {
        ActionResult<TE> result = new ActionResult<TE>();
        var any = await _dbSet.AnyAsync(p => p.Name == model.Name || p.Id== model.Id);
        
        if (any) result.Errors.Add("El elemento ya existe");
        
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        TE entity = new TE();
         model.ToEntity(entity);
        try
        {
            _dbSet.Add(entity);
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

    public async Task<ActionResult<TE>> Update(TM model)
    {
        ActionResult<TE> result = new ActionResult<TE>();
        
        var entity = await _dbSet.FindAsync(model.Id);
        
        if (entity == null)
        {
            result.Errors.Add("No se encuentra el elemento");
            result.CreateResponseInvalidAction();
            return result;
        }
        
        model.ToEntity(entity);
        try
        {
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

    public async Task<ActionResult<IList<TE>>> Delete(IList<TE> entities)
    {
        ActionResult<IList<TE>> result = new ActionResult<IList<TE>>();
        
        try
        {
            _dbSet.RemoveRange(entities);
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

    public async Task<IEnumerable<TE>> Find()
    {
        try
        {
            var list =  _dbSet.OrderBy(e=>e.Name).AsEnumerable();
            return  list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return  Enumerable.Empty<TE>();
        }
    }
}