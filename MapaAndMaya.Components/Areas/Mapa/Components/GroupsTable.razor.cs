using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Radzen;
using Radzen.Blazor;
using Group = MapaAndMaya.Services.Models.Group;

namespace MapaAndMaya.Components.Areas.Mapa.Components;

public partial class GroupsTable
{
    [Parameter] public CourseInCumFum Data { get; set; } = new();
     public RadzenDataGrid<Group> _grid { get; set; }

    private GroupToCourseRequest ViewModel { get; set; } = new();
   
    private async void AddItem()
    {
        ViewModel = new GroupToCourseRequest();
        ViewModel.CourseInCumFumId = Data.Id;
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Grupo");
    }

    private async void AddFormSubmit()
    {
        try
        {
            DialogService.Close();
            var openDialogTask = BusyDialog("Guardando ...");

            var response =  await GroupService.Create(ViewModel);
            if (response.Status)
            {
                if (response.Element != null) Data.Groups.Add(response.Element);
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
    
    private async void EditGroup(Group item)
    {
        ViewModel = new GroupToCourseRequest
        {
            Id = item.Id,
            CourseInCumFumId = item.CourseInCumFumId,
            AcademicYear = item.AcademicYear,
            AcademicCourse = item.AcademicCourse,
            Enrollment = item.Enrollment
        };
        await ShowFormularyDialog(EditFormSubmit, "Editar Facultad");

    }

    private async void EditFormSubmit()
    {
        try
        {
            DialogService.Close();
            var openDialogTask = BusyDialog("Guardando ...");

            var response =  await GroupService.Update(ViewModel);
            if (response.Status)
            {
                if (response.Element != null)
                    Data.Groups.First(fa => fa.Id == response.Element.Id).Clone(response.Element);
                
                NotifyOk(response.Title);
            }
            else
                NotifyErrors(response.Title, response.Errors);
        }
        catch (Exception e)
        {
            DialogService.Close();
            Logger.LogError(e.Message);
            throw;
        }
    }

    private async void DeleteGroup(Group item)
    {
        var confirmResult = await DialogService.Confirm("Confirma  eliminar el grupo?",
            "Confirmación", new ConfirmOptions { OkButtonText = "Si", CancelButtonText = "No" });

        if (!confirmResult.HasValue || !confirmResult.Value) return;
        
        var openDialogTask = BusyDialog("Eliminando ...");
        var response = await GroupService.Delete(item);
        if (response.Status)
        {
            if (response.Element != null) Data.Groups.Remove(response.Element);
                
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
        StateHasChanged();
        NotificationService.Notify(NotificationSeverity.Info, title);
    }
    
    private  void NotifyErrors( string title , List<string> errors)
    {
        foreach (var error  in errors) 
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = title, Detail = error, CloseOnClick = true, Duration = 5000, Style = "width: 400px;" });
        
        DialogService.Close();
    }
    
}