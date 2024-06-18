using MapaAndMaya.Services;
using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.ViewModels;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Degrees.Pages;

public partial class Degrees
{
    private bool _isLoading;

    private RadzenDataGrid<Degree> _grid = new();

    private IEnumerable<Degree> ItemsCollection { get; set; } = new List<Degree>();

    private IList<Degree>? SelectedItems { get; set; } = new List<Degree>();

    private GenericViewModel DegreeViewModel { get; set; } = new();

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) ReloadGridButton();

        return base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        ItemsCollection = _degreeService.Find();
        _isLoading = false;
    }

    private async void AddItem()
    {
        DegreeViewModel = new GenericViewModel();
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Carrera");
    }

    private async void AddFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _degreeService.Create(DegreeViewModel);
        if (response.Status && response.Element != null)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void EditItem(Degree item)
    {
        DegreeViewModel = new GenericViewModel()
        {
            Id = item.Id,
            Name = item.Name
        };
        await ShowFormularyDialog(EditFormSubmit, "Editar Facultad");
    }

    private async void EditFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _degreeService.Update(DegreeViewModel);

        if (response.Status)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void DeleteItems()
    {
        var confirmResult = await _dialogService.Confirm("Confirma  eliminar los elementos seleccionados?",
            "Confirmación", new ConfirmOptions { OkButtonText = "Si", CancelButtonText = "No" });

        if (confirmResult.HasValue && confirmResult.Value)
        {
            var openDialogTask = BusyDialog("Eliminando ...");

            if (SelectedItems != null)
            {
                var response = await _degreeService.Delete(SelectedItems);
                if (response.Status)
                    NotifyOk(response.Title);
                else
                    NotifyErrors(response.Title, response.Errors);
            }

            SelectedItems = new List<Degree>();
        }
    }

    private async void ReloadGridButton()
    {
        SelectedItems = new List<Degree>();
        _grid.Reset(true);
        await _grid.FirstPage(true);
    }

    private async void NotifyOk(string title)
    {
        await _grid.Reload();
        _dialogService.Close();
        _notificationService.Notify(NotificationSeverity.Info, title);
    }

    private void NotifyErrors(string title, List<string> errors)
    {
        _dialogService.Close();
        foreach (var error in errors)
            _notificationService.Notify(new NotificationMessage()
            {
                Severity = NotificationSeverity.Error, Summary = title, Detail = error, CloseOnClick = true,
                Duration = 5000, Style = "width: 400px;"
            });
    }
}