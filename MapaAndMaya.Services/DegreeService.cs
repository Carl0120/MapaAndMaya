﻿using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class DegreeService : ICrudService<Degree,GenericViewModel>
{
    private readonly ILogger<DegreeService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public DegreeService(ILogger<DegreeService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<ActionResult<Degree>> Create(GenericViewModel model)
    {
        ActionResult<Degree> result = new ActionResult<Degree>();
        var any = await _dbContext.Degrees.AnyAsync(p => p.Name == model.Name || p.Id== model.Id);
        
        if ( any) result.Errors.Add("La Carrera ya existe");
        
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        Degree entity = new Degree();
        entity.Name = model.Name;
        try
        {
            _dbContext.Degrees.Add(entity);
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
    
    public async Task<ActionResult<Degree>> Update(GenericViewModel model)
    {
        ActionResult<Degree> result = new ActionResult<Degree>();
        
        var entity = await _dbContext.Degrees.FindAsync(model.Id);
        
        if (entity == null)
        {
            result.Errors.Add("No se encuentra la facultad");
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

    public async Task<IEnumerable<Degree>> Find()
    {
        try
        {
            var list =  _dbContext.Degrees.OrderBy(e=>e.Name).AsEnumerable();
            return  list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return  Enumerable.Empty<Degree>();
        }
    }
    
    public async Task<ActionResult<IList<Degree>>> Delete(IList<Degree> degrees)
    {
        ActionResult<IList<Degree>> result = new ActionResult<IList<Degree>>();
        
        try
        {
          _dbContext.Degrees.RemoveRange(degrees);
            await _dbContext.SaveChangesAsync();
            result.CreateResponseSuccess(degrees);
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