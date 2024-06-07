using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
 
namespace MapaAndMaya.Services.NomenclatureServices;


public abstract class GenericService<TE, TM> : ICrudService<TE, TM>
    where TE : NomenclatureBase, new() where TM : GenericViewModel
{
  
    protected readonly ILogger<GenericService<TE, TM>> _logger;

    protected readonly MapaAndMayaDbContext _dbContext;

    private readonly DbSet<TE> _dbSet;
    
    protected GenericService(ILogger<GenericService<TE, TM>> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TE>();
    }
    
    public virtual async Task<ActionResult<TE>> Create(TM model)
    {
        
        ActionResult<TE> result = new();
        var any = await _dbSet.AnyAsync(p => p.Name == model.Name || p.Id == model.Id);

        if (any) result.Errors.Add("El elemento ya existe");
        
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

       
        var entity = new TE();
        model.ToEntity(entity);
        try
        {
            EntityEntry<TE> entityEntry = _dbSet.Add(entity);
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

    public async Task<ActionResult<TE>> Update(TM model)
    {
        
        ActionResult<TE> result = new();

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
        
        ActionResult<IList<TE>> result = new();

        var ids = entities.Select(e => e.Id).ToList();
        try
        {
            await _dbSet.Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync();
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
    
    public IEnumerable<TE> Find()
    {
        try
        {
            var list = _dbSet.OrderBy(e => e.Name).AsEnumerable();
            return list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Enumerable.Empty<TE>();
        }
    }
}