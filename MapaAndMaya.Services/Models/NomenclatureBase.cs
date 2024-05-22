using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public abstract class NomenclatureBase
{
    public int Id { get; init; }

    [MaxLength(50)] public string Name { get; set; } = "";
}