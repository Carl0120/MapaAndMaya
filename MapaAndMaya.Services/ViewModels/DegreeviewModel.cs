using System.ComponentModel.DataAnnotations;
using MapaAndMaya.Services.Models;

namespace MapaAndMaya.Services.ViewModels;

public class DegreeViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\süÜáéíóúÁÉÍÓÚñÑüÜ]+$", ErrorMessage = "El campo nombre debe contener solo letras")]
    [MaxLength(50, ErrorMessage = "El campo Nombre no debe tener más de 50 caracteres.")]
    public string Name { get; set; }
    
    public AccreditationStatus? AccreditationStatus { get; set; }
    
    [Required(ErrorMessage = "El campo Facultad es obligatorio")]
    public int FacultyId { get; set; }
}