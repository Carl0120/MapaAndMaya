using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Radzen;
using Radzen.Blazor;
using Group = MapaAndMaya.Services.Models.Group;

namespace MapaAndMaya.Components.Areas.Mapa.Pages;

public partial class Courses
{
    
    private bool _isLoading;
    
    private RadzenDataGrid<Course> _grid = new();
    
    private ICollection<Course> ItemsCollection { get; set; } = new List<Course>();
    
    private IList<Course>? SelectedItems { get; set; }

    private DegreeViewModel ItemViewModel { get; set; } = new();
    
    private ICollection<Course> ItemList { get; set; } = new List<Course>();

    protected  override async Task OnInitializedAsync()
    {
        _isLoading = true;
        cumFums = await  CumFumService.FindAll();
        ItemsCollection = await CourseService.Get(cumFumId);
        _isLoading = false;
        await  base.OnInitializedAsync();
    }

    private async void AddItem()
    {
       
    }

    private async void AddFormSubmit()
    {
        
    }

    private async void EditItem(Course item)
    {
        
    }
    private async void EditFormSubmit(){}

    private async void DeleteItems()
    {
        
    }
    private async void EditGroup(Group item)
    {
        
    }
    private async void DeleteGroup(Group item)
    {
        
    }
    
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