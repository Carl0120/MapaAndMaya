﻿using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class DegreeService : ICrudService<Degree,DegreeViewModel>
{
    private readonly ILogger<FacultyService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public DegreeService(ILogger<FacultyService> logger, MapaAndMayaDbContext dbContext)
    {
        this._logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ActionResult<Degree>> Create(DegreeViewModel model)
    {
        var nameTask = _dbContext.Degrees.AnyAsync(e => e.Name == model.Name);
        var facultyTask = _dbContext.Faculties.AnyAsync(e => e.Id == model.FacultyId);
        
        ActionResult<Degree> result = new ActionResult<Degree>();
        
        if (await nameTask) result.Errors.Add("Ya existe una carrera con ese nombre");
        
        if (!await facultyTask) result.Errors.Add("La facultad a la que hace referencia no existe");
        
        if (result.Errors.Any())
        {
            result.Status = false;
            result.Severity = NotifySeverity.Warning;
            result.Title = "Accion Inválida";
            return result;
        }

        Degree entity = new Degree();
        model.CopyToEntity(entity);
        
        try
        {
            EntityEntry<Degree> resp = _dbContext.Degrees.Add(entity);
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

    public async Task<ActionResult<Degree>> Update(DegreeViewModel model)
    {
       
        var findTask = _dbContext.Degrees.FindAsync(model.Id);
        var facultyTask = _dbContext.Faculties.AnyAsync(e => e.Id == model.FacultyId);
        
        ActionResult<Degree> result = new ActionResult<Degree>();
        
        Degree? entity = await findTask;
        
        if (entity == null) result.Errors.Add("No se encuentra la Carrera");
        
        if (!await facultyTask) result.Errors.Add("La facultad a la que hace referencia no existe");
        
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
            _dbContext.Degrees.Update(entity!);
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

    public async Task<ActionResult<Degree>> Delete(Degree entity)
    {
        ActionResult<Degree> result = new ActionResult<Degree>();
        
        try
        {
            Degree? item = await _dbContext.Degrees.FindAsync(entity.Id);
            if (item != null)
            {
                _dbContext.Degrees.Remove(item);
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

    public async Task<ICollection<Degree>> FindAll()
    {
        try
        {
            var listTask =  _dbContext.Degrees.AsNoTracking()
                .Include(e=>e.Faculty).ToListAsync();
            
            return await listTask;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<Degree>();
        }
    }

    public async Task<Degree?> Find(int id)
    {
        try
        {
            var itemTask = _dbContext.Degrees.Include(e=>e.Faculty)
                .FirstOrDefaultAsync(e => e.Id == id);
            return await itemTask;

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    
    public async Task<ICollection<Degree>> FindByFaculty(int facultyId)
    {
        try
        {
            var listTask =  _dbContext.Degrees.AsNoTracking()
                .Include(e=>e.Faculty)
                .Where(e =>e.FacultyId == facultyId)
                .ToListAsync();
            
            return await listTask;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<Degree>();
        }
    }
}