using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class FacultyService
{
    private readonly ILogger<FacultyService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public FacultyService(ILogger<FacultyService> logger, MapaAndMayaDbContext dbContext)
    {
        this._logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ActionResult<FacultyViewModel>> Create(FacultyViewModel model)
    {
        var anyTask = _dbContext.Faculties.AnyAsync(p => p.Name == model.Name);
        
        ActionResult<FacultyViewModel> result = new ActionResult<FacultyViewModel>(model);
        
        if (await anyTask)
        {
            result.Errors.Add(("Name","Ya existe una facultad con ese nombre"));
        }
        if (result.Errors.Any())
        {
            result.Status = false;
            result.Title = "Accion Inválida";
            return result;
        }

        Faculty entity = new Faculty();
        model.CopyToEntity(entity);
        
        try
        {
            _dbContext.Faculties.Add(entity);
            await _dbContext.SaveChangesAsync();
            result.Title = "Exito";
            result.Status = true;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.Title = "Fallo";
            result.Status = false;
            result.Errors.Add(("Server",ex.Message));
            return result;
        }

    }
    
    public async Task<ActionResult<FacultyViewModel>> Update(FacultyViewModel model)
    {
        var findTask = _dbContext.Faculties.FindAsync(model.Id);
        
        ActionResult<FacultyViewModel> result = new ActionResult<FacultyViewModel>(model);
        
        Faculty? entity = await findTask;
        
        if (entity == null)
        {
            result.Errors.Add(("Id","No se encuentra la facultad"));
        }
        
        if (result.Errors.Any())
        {
            result.Status = false;
            result.Title = "Accion Inválida";
            return result;
        }
      
        model.CopyToEntity(entity!);
        try
        {
            _dbContext.Faculties.Add(entity!);
            await _dbContext.SaveChangesAsync();
            result.Title = "Exito";
            result.Status = true;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.Title = "Fallo";
            result.Status = false;
            result.Errors.Add(("Server",ex.Message));
            return result;
        }

    }

    public async Task<ActionResult<Faculty>> Delete(Faculty entity)
    {
        ActionResult<Faculty> result = new ActionResult<Faculty>(entity);
        
        try
        {
            var item = await _dbContext.Faculties.FindAsync(entity.Id);
            _dbContext.Faculties.Remove(item);
            await _dbContext.SaveChangesAsync();
            result.Status = true;
            result.Title = "Exito";
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result.Title = "Fallo";
            result.Status = false;
            result.Errors.Add(("Server",ex.Message));
            return result;
        }
        
    }
    
    public async Task<IEnumerable<Faculty>> FindAll()
    {
        try
        {
            var listTask =  _dbContext.Faculties.AsNoTracking().ToListAsync();
            
            return await listTask;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Enumerable.Empty<Faculty>();
        }
    }

    public async Task<Faculty?> Find(int id)
    {
        try
        {
            var itemTask = _dbContext.Faculties.AsNoTracking()
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