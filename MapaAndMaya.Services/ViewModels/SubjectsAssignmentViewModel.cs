using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.ViewModels;

public class SubjectsAssignmentViewModel
{
    [Required(ErrorMessage = "Debe Seleccionar al menos una Asignatura")]
    public List<int> SubjectsIdList { get; set; } = new();

    public int PeriodInYearId { get; set; }
}