﻿using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Data.DB;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services.Services.NomenclatureServices;

public class ModalityService : GenericService<Modality, GenericViewModel>
{
    public ModalityService(ILogger<GenericService<Modality, GenericViewModel>> logger, MapaAndMayaDbContext dbContext) :
        base(logger, dbContext)
    {
    }

    public override async Task<ActionResult<Modality>> Create(GenericViewModel model)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            var result = await base.Create(model);
            if (!result.Status || result.Element == null)
                return result;

            try
            {
                var degrees = await _dbContext.Degrees.ToListAsync();

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
}