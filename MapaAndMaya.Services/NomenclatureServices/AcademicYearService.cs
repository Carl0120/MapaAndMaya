using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.NomenclatureServices;

public class AcademicYearService : ICrudService<AcademicYear, AcademicYearViewModel>
{
    protected readonly ILogger<AcademicYearService> _logger;

    protected readonly MapaAndMayaDbContext _dbContext;


    public AcademicYearService(ILogger<AcademicYearService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ActionResult<AcademicYear>> Create(AcademicYearViewModel model)
    {
        ActionResult<AcademicYear> result = new();
        var existAny = await _dbContext.AcademicYears.AnyAsync(p => p.Name == model.Name || p.Id == model.Id);
        var ordenAny = await _dbContext.AcademicYears.AnyAsync(p => p.Order == model.Order);

        if (existAny) result.Errors.Add("El elemento ya existe");
        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        var entity = new AcademicYear();
        model.ToEntity(entity);
        try
        {
            if (ordenAny) DesplazarOrden(model.Order);

            EntityEntry<AcademicYear> entityEntry = _dbContext.AcademicYears.Add(entity);
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

    public async Task<ActionResult<AcademicYear>> Update(AcademicYearViewModel model)
    {
        ActionResult<AcademicYear> result = new();

        var entity = await _dbContext.AcademicYears.AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.Id);
        var existAny = await _dbContext.AcademicYears.AnyAsync(p => p.Name == model.Name && p.Id != model.Id);
        var ordenAny = await _dbContext.AcademicYears.AnyAsync(e => e.Order == model.Order && e.Id != model.Id);

        if (entity == null) result.Errors.Add("No se encuentra el elemento");
        if (existAny) result.Errors.Add("El elemento ya existe");

        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        model.ToEntity(entity!);
        try
        {
            if (ordenAny) DesplazarOrden(model.Order);
            _dbContext.AcademicYears.Update(entity!);
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

    public async Task<ActionResult<IList<AcademicYear>>> Delete(IList<AcademicYear> entities)
    {
        ActionResult<IList<AcademicYear>> result = new();

        try
        {
            _dbContext.AcademicYears.RemoveRange(entities);
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

    public IEnumerable<AcademicYear> Find()
    {
        try
        {
            var list = _dbContext.AcademicYears.OrderBy(e => e.Order).AsEnumerable();
            return list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Enumerable.Empty<AcademicYear>();
        }
    }

    private async void DesplazarOrden(int order)
    {
        var elements = await _dbContext.AcademicYears.OrderBy(e => e.Order).Where(e => e.Order >= order).ToListAsync();

        foreach (var element in elements) element.Order++;
        await _dbContext.SaveChangesAsync();
    }
}