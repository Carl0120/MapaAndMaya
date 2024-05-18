using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class ModalityService :ICrudService<Modality,GenericViewModel>
{
    private readonly ILogger<ModalityService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public ModalityService(ILogger<ModalityService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<ActionResult<Modality>> Create(GenericViewModel model)
    {
        ActionResult<Modality> result = new ActionResult<Modality>();
        var any = await _dbContext.Modalities.AnyAsync(p => p.Name == model.Name || p.Id== model.Id);
        
        if (any) result.Errors.Add("La Modalidad ya existe");
        
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        Modality entity = new Modality();
        model.ToEntity(entity);
        try
        {
            _dbContext.Modalities.Add(entity);
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

    public async Task<ActionResult<Modality>> Update(GenericViewModel model)
    {
        ActionResult<Modality> result = new ActionResult<Modality>();
        
        var entity = await _dbContext.Modalities.FindAsync(model.Id);
        
        if (entity == null)
        {
            result.Errors.Add("No se encuentra la Modalidad");
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

    public async Task<ActionResult<IList<Modality>>> Delete(IList<Modality> entities)
    {
        ActionResult<IList<Modality>> result = new ActionResult<IList<Modality>>();
        
        try
        {
            _dbContext.Modalities.RemoveRange(entities);
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

    public async Task<IEnumerable<Modality>> Find()
    { try
        {
            var list =  _dbContext.Modalities.OrderBy(e=>e.Name).AsEnumerable();
            return  list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return  Enumerable.Empty<Modality>();
        }
    }
}