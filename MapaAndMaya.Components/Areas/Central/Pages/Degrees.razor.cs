using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Central.Pages;

public partial class Degrees
{
    private bool _isLoading;
    
    private RadzenDataGrid<Degree> _grid = new();
    
    private ICollection<Degree> DegreesCollection { get; set; } = new List<Degree>();
    
    private IList<Degree>? SelectedDegrees { get; set; }

    private DegreeViewModel DegreeViewModel { get; set; } = new();
    
    private ICollection<Faculty> FacultiesList { get; set; } = new List<Faculty>();
    
    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        DegreesCollection = await  DegreeService.FindAll();
        _isLoading = false;
    }

    private async void AddItem()
    {
        DegreeViewModel = new DegreeViewModel();
        
        FacultiesList = await FacultyService.FindAll();
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Carrera");
    }

    private async void AddFormSubmit()
    {
        
    }

    private async void EditItem(Degree item)
    {
        
    }
    private async void EditFormSubmit(){}
    private async void DeleteItems(){}
    
    private async void NotifyOk(string title)
    {
        await _grid.Reload();
        DialogService.Close();
        NotificationService.Notify(NotificationSeverity.Info, title);
    }
    
    private  void NotifyErrors( string title , List<string> errors)
    {
        foreach (var error  in errors) 
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = title, Detail = error, CloseOnClick = true, Duration = 5000, Style = "width: 400px;" });
        
        DialogService.Close();
    }
}