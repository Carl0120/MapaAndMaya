using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;

namespace MapaAndMaya.Services.Mappers;

public static class Extensions
{
    public static void ToEntity(this GenericViewModel model, NomenclatureBase entity)
    {
        entity.Name = model.Name;
    }

    public static void ToEntity(this AcademicYearViewModel model, AcademicYear entity)
    {
        entity.Name = model.Name;
        entity.Order = model.Order;
    }
}