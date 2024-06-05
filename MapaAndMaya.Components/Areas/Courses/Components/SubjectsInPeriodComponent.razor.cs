using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Courses.Components;

public partial class SubjectsInPeriodComponent
{
    [Parameter] public int PeriodInYearId { get; set; }
    
    private bool _courseNotFound;

    private PeriodInYear? PeriodInYear {get; set; }

    private ICollection<SubjectsInPeriod> ItemsCollection { get; set; } = new List<SubjectsInPeriod>();

    private IList<Subject> SubjectsList { get; set; } = new List<Subject>();
    
    private bool _isLoading;

    private bool _disableSwitch = false;
    private RadzenDataGrid<SubjectsInPeriod> _grid = new();
    
    private IList<SubjectsInPeriod>? SelectedItems { get; set; } = new List<SubjectsInPeriod>();

    private SubjectsAssignmentViewModel ViewModel { get; set; } = new();

    
    protected override async Task OnParametersSetAsync()
    {
        PeriodInYear = await _subjectsAssignmentService.GetPeriodInYear(PeriodInYearId);
        if (PeriodInYear == default)
        {
           
            _courseNotFound = true;
        }else{
            ItemsCollection = await _subjectsAssignmentService.GetSubjectsInPeriod(PeriodInYearId);
        }
        await base.OnParametersSetAsync();
    }

    private async void AddItem()
    {
        SubjectsList = _subjectService.Find().ToList();
        ViewModel = new SubjectsAssignmentViewModel
        {
            PeriodInYearId = PeriodInYearId
        };
        
        await ShowFormularyDialog(AddFormSubmit, "Adicionar Asignatura");
    }

    private async void AddFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _subjectsAssignmentService.Create(ViewModel);
       
        if (response.Status && response.Element != null)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void EditItem(SubjectsInPeriod item)
    {
    }

    private async void EditFormSubmit()
    {
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
                var response = await _subjectsAssignmentService.Delete(SelectedItems);
                if (response.Status)
                    NotifyOk(response.Title);
                else
                    NotifyErrors(response.Title, response.Errors);
            }

            SelectedItems = new List<SubjectsInPeriod>();
        }
    }
    
    private async void NotifyOk(string title)
    {
        ItemsCollection = await _subjectsAssignmentService.GetSubjectsInPeriod(PeriodInYearId);
        await _grid.Reload();
        _dialogService.Close();
        StateHasChanged();
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
    
    private async Task OnChange(SubjectsInPeriod data)
    {
        _disableSwitch = true;
       bool result = await _subjectsAssignmentService.ChangeFinalExam(data);
        //DisableButton = !ItemsChanged.Any();
        if (!result)
        {
            data.HaveFinalExam = !data.HaveFinalExam;
            _notificationService.Notify(new NotificationMessage()
            {
                Severity = NotificationSeverity.Error, Summary = "Error", Detail =" No se pudo completar la acción", CloseOnClick = true,
                Duration = 5000, Style = "width: 400px;"
            });
        }
        _disableSwitch = false;
        StateHasChanged();
    }
}