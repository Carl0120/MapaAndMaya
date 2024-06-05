using MapaAndMaya.Services;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Subjects;

public partial class AcademicYears
{
    private bool _isLoading;
    private RadzenDataGrid<AcademicYear> _grid = new();
    private IEnumerable<AcademicYear> ItemsCollection { get; set; } = new List<AcademicYear>();
    private IList<AcademicYear>? SelectedItems { get; set; } = new List<AcademicYear>();
    private AcademicYearViewModel ViewModel { get; set; } = new();

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) ReloadGridButton();
        return base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        ItemsCollection = _academicYearService.Find();
        _isLoading = false;
    }

    private async void AddItem()
    {
        ViewModel = new AcademicYearViewModel();
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Año Académico");
    }

    private async void AddFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _academicYearService.Create(ViewModel);
        if (response.Status)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void EditItem(AcademicYear item)
    {
        ViewModel = new AcademicYearViewModel()
        {
            Id = item.Id,
            Name = item.Name,
            Order = item.Order
        };
        await ShowFormularyDialog(EditFormSubmit, "Editar Año Académico");
    }

    private async void EditFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _academicYearService.Update(ViewModel);

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
                var response = await _academicYearService.Delete(SelectedItems);
                if (response.Status)
                    NotifyOk(response.Title);
                else
                    NotifyErrors(response.Title, response.Errors);
            }

            SelectedItems = new List<AcademicYear>();
        }
    }

    private async void ReloadGridButton()
    {
        SelectedItems = new List<AcademicYear>();
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