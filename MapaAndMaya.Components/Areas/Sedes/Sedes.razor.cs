using MapaAndMaya.Services;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Sedes;

public partial class Sedes
{
    
    private bool _isLoading;

    private RadzenDataGrid<Sede> _grid = new();
    
    private IEnumerable<Sede> ItemsCollection { get; set; } = new List<Sede>();
    
    private IEnumerable<SedeType> SedeTypesCollection { get; set; } = new List<SedeType>();
    
    private IEnumerable<Town> TownsCollection { get; set; } = new List<Town>();

    private IList<Sede>? SelectedItems { get; set; } = new List<Sede>();

    private SedeViewModel ViewModel { get; set; } = new();

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) ReloadGridButton();

        return base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        ItemsCollection = _sedeService.Find();
        SedeTypesCollection = _sedeTypeService.Find();
        TownsCollection = _townService.Find();
        _isLoading = false;
    }

    private async void AddItem()
    {
        ViewModel = new SedeViewModel();
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Sede");
    }

    private async void AddFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        ActionResult<Sede> response = await _sedeService.Create(ViewModel);
        if (response.Status)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void EditItem(Sede item)
    {
        ViewModel = SedeViewModel.Clone(item);
        
        await ShowFormularyDialog(EditFormSubmit, "Editar Sede");
    }

    private async void EditFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _sedeTypeService.Update(ViewModel);

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
                var response = await _sedeService.Delete(SelectedItems);
                if (response.Status)
                    NotifyOk(response.Title);
                else
                    NotifyErrors(response.Title, response.Errors);
            }

            SelectedItems = new List<Sede>();
        }
    }

    private async void ReloadGridButton()
    {
        SelectedItems = new List<Sede>();
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