using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.ViewModels;

public class DegreeViewModel
{
    
    public int Id { get; init; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\süÜáéíóúÁÉÍÓÚñÑüÜ]+$", ErrorMessage = "El campo nombre debe contener solo letras")]
    [MaxLength(50, ErrorMessage = "El campo Nombre no debe tener más de 50 caracteres.")]
    public string Name { get; set; } = "";
}

