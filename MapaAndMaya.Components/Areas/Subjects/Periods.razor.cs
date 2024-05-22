using MapaAndMaya.Services;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Subjects;

public partial class Periods
{
    private bool _isLoading;
    private RadzenDataGrid<Period> _grid = new();
    private IEnumerable<Period> ItemsCollection { get; set; } = new List<Period>();
    private IList<Period>? SelectedItems { get; set; } = new List<Period>();
    private GenericViewModel ViewModel { get; set; } = new();

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) ReloadGridButton();

        return base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        ItemsCollection = _periodService.Find();
        _isLoading = false;
    }

    private async void AddItem()
    {
        ViewModel = new GenericViewModel();
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Periodo");
    }

    private async void AddFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        ActionResult<Period> response = await _periodService.Create(ViewModel);
        if (response.Status)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void EditItem(Period item)
    {
        ViewModel = new GenericViewModel()
        {
            Id = item.Id,
            Name = item.Name
        };
        await ShowFormularyDialog(EditFormSubmit, "Editar Periodo");
    }

    private async void EditFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _periodService.Update(ViewModel);

        if (response.Status)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void DeleteItems()
    {
        var confirmResult = await _dialogService.Confirm("Confirma eliminar los elementos seleccionados?",
            "Confirmación", new ConfirmOptions { OkButtonText = "Si", CancelButtonText = "No" });

        if (confirmResult.HasValue && confirmResult.Value)
        {
            var openDialogTask = BusyDialog("Eliminando ...");

            if (SelectedItems != null)
            {
                var response = await _periodService.Delete(SelectedItems);
                if (response.Status)
                    NotifyOk(response.Title);
                else
                    NotifyErrors(response.Title, response.Errors);
            }

            SelectedItems = new List<Period>();
        }
    }

    private async void ReloadGridButton()
    {
        SelectedItems = new List<Period>();
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
                Severity = NotificationSeverity.Error,
                Summary = title,
                Detail = error,
                CloseOnClick = true,
                Duration = 5000,
                Style = "width: 400px;"
            });
    }
}