using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Courses;

public partial class YearsInCourses
{
    [Parameter] public int CourseId { get; set; }

    private bool _courseNotFound;

    private Course? Course { get; set; } = new Course();
    
    private ICollection<YearsInCourse> YearsInCourseCollection { get; set; } = new List<YearsInCourse>();

    private List<AcademicYear> AcademicYearsList { get; set; } = new List<AcademicYear>();
    
    private List<Period> PeriodList { get; set; } = new List<Period>();

    private YearsAssignmentViewModel ViewModel { get; set; } = new();

     RadzenTabs Tabs { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Course = _courseService.Find(CourseId);

        if (Course == default)
        {
           
            _courseNotFound = true;
        }
        else
        {
            YearsInCourseCollection = await _yearsAssignmentService.GetYearsInCourse(Course.Id);
        }
        
        await base.OnInitializedAsync();
    }

    private async void AddItem()
    {
        if (Course != null)
        {
            AcademicYearsList = await _yearsAssignmentService.GetWhitYearsInCourse(Course.Id);
            PeriodList =  _periodService.Find().ToList();
            ViewModel = new()
            {
                CourseId = Course.Id
            };
            await ShowFormularyDialog(AddFormSubmit, "Adicionar Año Acaémico");
        }
    }

    private async void AddFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _yearsAssignmentService.Create(ViewModel);
        if (response.Status && response.Element != null)
        {
          
            NotifyOk(response.Title);
        }
        else
        {
            NotifyErrors(response.Title, response.Errors);
        }
    }
    
    private async void DeleteItems(YearsInCourse item)
    {
        var confirmResult = await _dialogService.Confirm("Confirma  eliminar el elemento seleccionado?",
            "Confirmación", new ConfirmOptions { OkButtonText = "Si", CancelButtonText = "No" });

        if (confirmResult.HasValue && confirmResult.Value)
        {
            var openDialogTask = BusyDialog("Eliminando ...");

           
                var response = await _yearsAssignmentService.Delete(item);
                if (response.Status)
                {
                    NotifyOk(response.Title);
                    Tabs.Reload();
                }
                else
                    NotifyErrors(response.Title, response.Errors);
                
        }
    }

    private async void NotifyOk(string title)
    {
        if (Course != null) YearsInCourseCollection = await _yearsAssignmentService.GetYearsInCourse(Course.Id);
        Tabs.Reload();
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