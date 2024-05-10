
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Mapa.Pages;

public partial class Courses
{

    [Parameter] public int CumFumId { get; set; }

    private IEnumerable<CumFum> CumFums { get; set; } = new List<CumFum>();

    private bool _isLoading;
    
    private RadzenDataGrid<CourseInCumFum> _grid = new();
    
    private ICollection<CourseInCumFum> ItemsCollection { get; set; } = new List<CourseInCumFum>();
    
    private AddCourseToCumFumRequest AddFormViewModel { get; set; } = new();
    

    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;
        CumFums = await  CumFumService.FindAll();
        if (CumFumId != 0)
        {
            ItemsCollection = await CourseService.GetCourseByCumFum(CumFumId);
        }
        else
        {
            ItemsCollection =new List<CourseInCumFum>();
        }
        
        _isLoading = false;
        await base.OnParametersSetAsync();
    }


    private async void AddItem()
    {
        AddFormViewModel = new AddCourseToCumFumRequest();
        AddFormViewModel.CumFumId = CumFumId;
        ICollection<Degree> degreeWithCourses = await  CourseService.GetDegreeWithCourses();
        await ShowFormularyDialog(AddFormSubmit, "Adicionar curso al CUM-FUM",degreeWithCourses);

    }

    private async void AddFormSubmit()
    {
        try
        {
            DialogService.Close();
            var openDialogTask = BusyDialog("Guardando ...");

            var response =  await CourseService.AddCourseToCumFum(AddFormViewModel);
            if (response.Status)
            {
                if (response.Element != null) ItemsCollection.Add(response.Element);
                NotifyOk(response.Title);
            }
            else
                NotifyErrors(response.Title ,response.Errors);
        }
        catch (Exception e)
        { 
            DialogService.Close();
            Logger.LogError(e.Message);
            throw;
        }
    }
    
    private async void DeleteItem(CourseInCumFum item)
    {
        var confirmResult = await DialogService.Confirm("Confirma  eliminar el curso seleccionado y todos sus grupos",
            "Confirmación", new ConfirmOptions { OkButtonText = "Si", CancelButtonText = "No" });

        if (!confirmResult.HasValue || !confirmResult.Value) return;
        
        var openDialogTask = BusyDialog("Eliminando ...");
        var response = await CourseService.Delete(item);
        if (response.Status)
        {
            if (response.Element != null) ItemsCollection.Remove(response.Element);
                
            NotifyOk(response.Title);
        }
        else
        {
            NotifyErrors(response.Title, response.Errors);
        }
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