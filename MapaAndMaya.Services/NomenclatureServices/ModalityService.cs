using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.Repositories;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.NomenclatureServices;

public class ModalityService :ICrudService<Modality,GenericViewModel>
{
    public ModalityService(ILogger<GenericService<Modality, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) :
        base(logger, dbContext)
    {
    }

    public override async Task<ActionResult<Modality>> Create(GenericViewModel model)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            ActionResult<Modality> result = await base.Create(model);
            if (!result.Status || result.Element == null)
                return result;

            try
            {
                List<Degree> degrees = await _dbContext.Degrees.ToListAsync();

                var degreeModalities = degrees.Select(m => new DegreeModality
                {
                    DegreeId = m.Id,
                    ModalityId = result.Element!.Id
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

    public Task<ActionResult<Modality>> Update(GenericViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IList<Modality>>> Delete(IList<Modality> entities)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Modality> Find()
    {
        throw new NotImplementedException();
    }
}