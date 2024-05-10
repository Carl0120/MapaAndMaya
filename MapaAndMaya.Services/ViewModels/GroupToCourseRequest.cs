using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.ViewModels;

public class GroupToCourseRequest
{
    public int Id { get; set; }
    
    public int CourseInCumFumId { get; set; }
    
    [Range(minimum: 1000,maximum: 9999,ErrorMessage = "El curso académico debe tener 4 digitos")]
    public int AcademicCourse { get; set; }
    
    [Range(minimum: 1,9, ErrorMessage = "El curso académico debe tener 1 digitos")]
    public int AcademicYear { get; set;}
    
    [Range(1,10000, ErrorMessage = "La matricula debe ser mayor que 0")]
    public int Enrollment { get; set; }
}