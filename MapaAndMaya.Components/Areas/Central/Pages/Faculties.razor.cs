using MapaAndMaya.Services;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.Extensions.Logging;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Central.Pages;

public partial class Faculties
{

    private bool _isLoading;
    
    private RadzenDataGrid<Faculty> _grid = new();
    
    private ICollection<Faculty> FacultiesCollection { get; set; } = new List<Faculty>();
    
    private IList<Faculty>? SelectedFaculties { get; set; } = new List<Faculty>();

    private FacultyViewModel FacultyViewModel { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        FacultiesCollection = await FacultyService.FindAll();
        _isLoading = false;
    }

    private async void AddItem()
    {
        FacultyViewModel = new FacultyViewModel();
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Facultad");
    }

    private async void AddFormSubmit()
    {
        try
        {
            DialogService.Close();
            var openDialogTask = BusyDialog("Guardando ...");

            ActionResult<Faculty> response =  await FacultyService.Create(FacultyViewModel);
            if (response.Status)
            { 
                FacultiesCollection.Add(response.Element);
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

    private async void EditItem(Faculty item)
    {
           
            FacultyViewModel = new FacultyViewModel
            {
                Id = item.Id,
                Name = item.Name
            };
            await ShowFormularyDialog(EditFormSubmit, "Editar Facultad");
        
    }

    private async void EditFormSubmit()
    {
        try
        {
            DialogService.Close();
            var openDialogTask = BusyDialog("Guardando ...");

            var response =  await FacultyService.Update(FacultyViewModel);
            if (response.Status)
            {
                if (response.Element != null)
                    FacultiesCollection.First(fa => fa.Id == response.Element.Id).Name = response.Element.Name;
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

    private async void DeleteItems()
    {
        try
        {
            var confirmResult = await DialogService.Confirm("Confirma  eliminar los elementos seleccionados?",
                "Confirmación", new ConfirmOptions { OkButtonText = "Si", CancelButtonText = "No" });

            if (confirmResult.HasValue && confirmResult.Value)
            {
                var openDialogTask = BusyDialog("Eliminando ...");

                if (SelectedFaculties != null)
                {
                    
                    foreach (var item in SelectedFaculties)
                    {
                        var response = await FacultyService.Delete(item);
                        if (response.Status)
                        {
                           
                            FacultiesCollection.Remove(response.Element);
                            NotifyOk(response.Title);
                        }
                        else
                            NotifyErrors(response.Title, response.Errors);
                    }
                }
                SelectedFaculties = new List<Faculty>(); 
            }
        }
        catch (Exception e)
        {
            DialogService.Close();
            Logger.LogError(e.Message);
            throw;
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
