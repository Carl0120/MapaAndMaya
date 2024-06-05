using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.ViewModels;

public class YearsAssignmentViewModel
{
    public int Id { get; init; }
    
    public int CourseId { get; set; }
    
    [Range(1,int.MaxValue, ErrorMessage = "Debe seleccionar un Año Académico")]
    public int AcademicYearId { get; set; }

    [Required(ErrorMessage = "Debe Seleccionar al menos un periodo o más")]
    public List<int> PeriodsId { get; set; } = new();
}