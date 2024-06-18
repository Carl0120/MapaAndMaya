using MapaAndMaya.Services.Core.Contracts;
using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Services.NomenclatureServices;

public class AcademicYearService : ICrudService<AcademicYear, AcademicYearViewModel>
{
    private readonly MapaAndMayaDbContext _dbContext;
    private readonly ILogger<AcademicYearService> _logger;


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
            if (ordenAny)
            {
                var years = await _dbContext.AcademicYears.ToListAsync();
                DesplazarOrder(years, entity.Order);
            }

            var entityEntry = _dbContext.AcademicYears.Add(entity);
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

        var exist = await _dbContext.AcademicYears.AnyAsync(e => e.Id == model.Id);
        var existAny = await _dbContext.AcademicYears.AnyAsync(p => p.Name == model.Name && p.Id != model.Id);

        if (!exist) result.Errors.Add("No se encuentra el elemento");

        if (existAny) result.Errors.Add("El nombre ya esta en uso");

        if (result.Errors.Any())
        {
            result.CreateResponseInvalidAction();
            return result;
        }

        var years = await _dbContext.AcademicYears.ToListAsync();
        DesplazarOrder(years, model.Order);

        var year = years.Find(e => e.Id == model.Id);

        if (year != null) model.ToEntity(year);
        try
        {
            await _dbContext.SaveChangesAsync();
            result.CreateResponseSuccess(year!);
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

    private async void DesplazarOrder(List<AcademicYear> years, int order)
    {
        foreach (var element in years.Where(e => e.Order >= order).ToList())
            element.Order++;
    }

    public async void ActualizarOrdenes()
    {
        var years = await _dbContext.AcademicYears.ToListAsync();
        years.Sort((x, y) => x.Order.CompareTo(y.Order));

        for (var i = 0; i < years.Count; i++) years[i].Order = i + 1;

        await _dbContext.SaveChangesAsync();
    }
}