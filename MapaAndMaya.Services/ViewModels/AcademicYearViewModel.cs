using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.ViewModels;

public class AcademicYearViewModel : GenericViewModel
{
    [Required(ErrorMessage = "El campo Orden es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El campo orden debe ser mayor o igual a cero")]
    public int Order { get; set; }
}