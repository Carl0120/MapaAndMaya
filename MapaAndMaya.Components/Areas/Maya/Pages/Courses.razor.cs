using MapaAndMaya.Services;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Maya.Pages;

public partial class Courses
{
    [Parameter] public int FacultyId { get; set; }

    private Faculty? Faculty { get; set; }
    private string Title { get; set; } = "";

    private bool _isLoading;
    
    private RadzenDataGrid<Course> _grid = new();
    
    private ICollection<Course> ItemsCollection { get; set; } = new List<Course>();
    
    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;
        Faculty = await  _FacultyService.Find(FacultyId);
        if (Faculty != null)
        {
            ItemsCollection = await _SubjectInCourseService.GetCourseByFaculty(FacultyId);
            Title = $"Cursos de la {Faculty.Name}";
        }
        else
        {
            Title = "La Facultad a la que intenta hacer Rreferencia no existe";
            ItemsCollection =new List<Course>();
        }
        
        _isLoading = false;
        await base.OnParametersSetAsync();
    }
    
}