using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.ViewModels;

public class FacultyViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\süÜáéíóúÁÉÍÓÚñÑüÜ]+$", ErrorMessage = "El campo nombre debe contener solo letras")]
    [MaxLength(50, ErrorMessage = "El campo Nombre no debe tener más de 50 caracteres.")]
    [MinLength(5, ErrorMessage = "El campo Nombre no debe tener menos de 5 caracteres.")]
    public string Name { get; set; } = "";
}