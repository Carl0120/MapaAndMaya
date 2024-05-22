using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.ViewModels.Validators;

public class UniqueValueValidator : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return base.IsValid(value);
    }
}