using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.Repositories;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.NomenclatureServices;

public class DegreeService : GenericService<Degree, GenericViewModel>
{
    public DegreeService(ILogger<GenericService<Degree, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) :
        base(logger, dbContext)
    {
    }

    public override async Task<ActionResult<Degree>> Create(GenericViewModel model)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            ActionResult<Degree> result = await base.Create(model);
            if (!result.Status || result.Element == null)
                return result;

            try
            {
                List<Modality> modalities = await _dbContext.Modalities.ToListAsync();

                var degreeModalities = modalities.Select(m => new DegreeModality
                {
                    DegreeId = result.Element!.Id,
                    ModalityId = m.Id
                }).ToList();

                _dbContext.DegreeModalities.AddRange(degreeModalities);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message);
                result.CreateResponseFail(ex);
                return result;
            }
        }
    }

    public  async Task<IEnumerable<Degree>> GetWhitModality()
    {
        try
        {
            var list = await _dbContext.Degrees.OrderBy(e => e.Name)
                .Include(e=>e.DegreeModalities)
                .ThenInclude(e=>e.Modality)
                .ToListAsync();
            return list;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<Degree>();
        }
    }
}