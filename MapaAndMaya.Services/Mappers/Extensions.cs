using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;

namespace MapaAndMaya.Services.Mappers;

public static class Extensions
{
    public static void ToEntity(this GenericViewModel model, Degree entity)
    {
        entity.Name = model.Name;
    }
    public static void ToEntity(this GenericViewModel model, Modality entity)
    {
        entity.Name = model.Name;
    }
   
    public static void ToEntity(this GenericViewModel model, NomenclatureBase entity)
    {
        entity.Name = model.Name;
    }
    
}