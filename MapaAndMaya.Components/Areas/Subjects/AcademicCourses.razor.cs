using MapaAndMaya.Services;
using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.Services;
using MapaAndMaya.Services.ViewModels;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Subjects;

public partial class AcademicCourses
{
    private bool _isLoading;
    private RadzenDataGrid<AcademicCourse> _grid = new();
    private IEnumerable<AcademicCourse> ItemsCollection { get; set; } = new List<AcademicCourse>();
    private IList<AcademicCourse>? SelectedItems { get; set; } = new List<AcademicCourse>();
    private GenericViewModel ViewModel { get; set; } = new();

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) ReloadGridButton();

        return base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        ItemsCollection = _academicCourseService.Find();
        _isLoading = false;
    }

    private async void AddItem()
    {
        ViewModel = new GenericViewModel();
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Curso Académico");
    }

    private async void AddFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        ActionResult<AcademicCourse> response = await _academicCourseService.Create(ViewModel);
        if (response.Status)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void EditItem(AcademicCourse item)
    {
        ViewModel = new GenericViewModel()
        {
            Id = item.Id,
            Name = item.Name
        };
        await ShowFormularyDialog(EditFormSubmit, "Editar Curso Académico");
    }

    private async void EditFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _academicCourseService.Update(ViewModel);

        if (response.Status)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void DeleteItems()
    {
        var confirmResult = await _dialogService.Confirm("¿Confirma eliminar los elementos seleccionados?",
            "Confirmación", new ConfirmOptions { OkButtonText = "Sí", CancelButtonText = "No" });

        if (confirmResult.HasValue && confirmResult.Value)
        {
            var openDialogTask = BusyDialog("Eliminando ...");

            if (SelectedItems != null)
            {
                var response = await _academicCourseService.Delete(SelectedItems);
                if (response.Status)
                    NotifyOk(response.Title);
                else
                    NotifyErrors(response.Title, response.Errors);
            }

            SelectedItems = new List<AcademicCourse>();
        }
    }

    private async void ReloadGridButton()
    {
        SelectedItems = new List<AcademicCourse>();
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