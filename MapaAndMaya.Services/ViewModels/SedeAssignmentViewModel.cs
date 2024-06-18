using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.ViewModels;

public class SedeAssignmentViewModel
{
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Debe Seleccionar al menos un CUM-FUM")]
    public List<int> SedeIdList { get; set; } = new();
}