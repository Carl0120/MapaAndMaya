using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class SubjectsAssignmentService
{
    private readonly ILogger<SubjectsAssignmentService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;
    
    public SubjectsAssignmentService(ILogger<SubjectsAssignmentService> logger, MapaAndMayaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

     public async Task<ActionResult<List<SubjectsInPeriod>>> Create(SubjectsAssignmentViewModel model)
    {
        ActionResult<List<SubjectsInPeriod>> result = new();
        var existList = await _dbContext.SubjectsInPeriods
            .Where(p => p.PeriodInYearId==model.PeriodInYearId && model.SubjectsIdList.Contains(p.SubjectId))
            .Select(e=>e.SubjectId)
            .ToListAsync();

        if (existList.Count >= model.SubjectsIdList.Count)
        {
            result.Errors.Add($"Las asignaturas ya existen en este Periodo");
            result.CreateResponseInvalidAction();
            return result;
        }
        
        foreach (var item in existList)
        {
            model.SubjectsIdList.Remove(item);
        }

        List<SubjectsInPeriod> entities = new List<SubjectsInPeriod>();
        model.ToEntity(entities);
         
            try
            {
                    await _dbContext.SubjectsInPeriods.AddRangeAsync(entities);
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
     
     public async Task<ActionResult<IList<SubjectsInPeriod>>> Delete(IList<SubjectsInPeriod> entities )
        {
            ActionResult<IList<SubjectsInPeriod>> result = new();

            var ids = entities.Select(e => e.Id).ToList();
            try
            {
                await _dbContext.SubjectsInPeriods.Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync();
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
     
     public async Task<ICollection<SubjectsInPeriod>> GetSubjectsInPeriod(int periodId)
        {
            try
            {
                var list = _dbContext.SubjectsInPeriods
                    .OrderBy(e => e.Subject.Name)
                    .Where(e=>e.PeriodInYearId == periodId)
                    .Include(e=>e.Subject)
                    .Include(e=>e.PeriodInYear)
                    .ToListAsync();
                return await list;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<SubjectsInPeriod>();
            }
        }
     
     public async Task<PeriodInYear?> GetPeriodInYear(int periodId)
        {
            try
            {
                var item = _dbContext.PeriodInYear
                    .Include(e=>e.Period)
                    .FirstAsync(e=>e.Id == periodId);
                return await item;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

     public async Task<bool> ChangeFinalExam(SubjectsInPeriod data)
        {
            try
            {
             
                _dbContext.Entry(data).Property(e => e.HaveFinalExam).IsModified = true;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            
            { _logger.LogError(ex.Message);
                
                return false;
            }
            
        }
}