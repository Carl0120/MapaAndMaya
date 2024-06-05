using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Courses;

public partial class Courses
{
    
    private bool _isLoading;

    private RadzenDataGrid<Course> _grid = new();

    private IEnumerable<Course> ItemsCollection { get; set; } = new List<Course>();
    
    private List<AcademicCourse> AcademicCourseCollection { get; set; } = new List<AcademicCourse>();
    
    private List<StudyPlan> StudyPlanCollection { get; set; } = new List<StudyPlan>();
    
    private List<Degree> DegreeCollection { get; set; } = new List<Degree>();

    private IList<Course>? SelectedItems { get; set; } = new List<Course>();

    private CourseViewModel ViewModel { get; set; } = new();

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) ReloadGridButton();

        return base.OnAfterRenderAsync(firstRender);
    }

    
    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        ItemsCollection = _courseService.Find();
        _isLoading = false;
       await base.OnInitializedAsync();
    }

    private async void AddItem()
    { 
        AcademicCourseCollection = _academicCourseService.Find().Reverse().ToList();
        StudyPlanCollection = _studyPlanService.Find().ToList();
        DegreeCollection = (List<Degree>) await _degreeService.GetWhitModality();
        ViewModel = new CourseViewModel();
        await ShowFormularyDialog(new List<DegreeModality>(),AddFormSubmit, "Adicionar Curso");
    }

    private async void AddFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _courseService.Create(ViewModel);
        if (response.Status && response.Element != null)
            NotifyOk(response.Title);
        else
            NotifyErrors(response.Title, response.Errors);
    }

    private async void EditItem(Course item)
    {
        AcademicCourseCollection = _academicCourseService.Find().Reverse().ToList();
        StudyPlanCollection = _studyPlanService.Find().ToList();
        DegreeCollection = (List<Degree> )await _degreeService.GetWhitModality();
        
        ViewModel =  CourseViewModel.Clone(item);
            ICollection<DegreeModality> degreeModalities =  DegreeCollection.First(e => e.Id == ViewModel.DegreeId).DegreeModalities;
        await ShowFormularyDialog(degreeModalities,EditFormSubmit, "Editar Curso");
        
    }

    private async void EditFormSubmit()
    {
        _dialogService.Close();
        var openDialogTask = BusyDialog("Guardando ...");

        var response = await _courseService.Update(ViewModel);

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
                var response = await _courseService.Delete(SelectedItems);
                if (response.Status)
                    NotifyOk(response.Title);
                else
                    NotifyErrors(response.Title, response.Errors);
            }

            SelectedItems = new List<Course>();
        }
    }

    private async void ReloadGridButton()
    {
        SelectedItems = new List<Course>();
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