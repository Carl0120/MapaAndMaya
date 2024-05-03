using MapaAndMaya.Services.Contracts;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class CumFumService : ICrudService<CumFum,CumFumViewModel>
{
    private readonly ILogger<FacultyService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public CumFumService(ILogger<FacultyService> logger, MapaAndMayaDbContext dbContext)
    {
        this._logger = logger;
        _dbContext = dbContext;
    }

    public Task<ActionResult<CumFum>> Create(CumFumViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<CumFum>> Update(CumFumViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<CumFum>> Delete(CumFum entity)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<CumFum>> FindAll()
    {
        try
        {
            var listTask =  _dbContext.CumFums.AsNoTracking().ToListAsync();
            
            return await listTask;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<CumFum>();
        }
    }

    public async Task<CumFum?> Find(int id)
    {
        try
        {
            var itemTask = _dbContext.CumFums
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
