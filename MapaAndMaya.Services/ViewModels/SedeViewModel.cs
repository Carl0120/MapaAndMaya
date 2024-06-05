using MapaAndMaya.Services.Models;

namespace MapaAndMaya.Services.ViewModels;

public class SedeViewModel : GenericViewModel
{
    public int TownId { get; set; }
    
    public int SedeTypeId { get; set; }
    
    public static SedeViewModel Clone( Sede entity)
    {
        return new SedeViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            TownId = entity.TownId,
            SedeTypeId = entity.SedeTypeId,
        };
    }
}