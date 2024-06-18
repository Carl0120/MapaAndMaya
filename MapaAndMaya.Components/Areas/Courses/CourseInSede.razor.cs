using MapaAndMaya.Services.Core.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Courses;

public partial class CourseInSede
{
    [Parameter] public int CourseId { get; set; }

    private bool _courseNotFound;
    
     private Course? Course { get; set; } = new Course();
     
     private ICollection<SedeCourse> ItemsCollection { get; set; } = new List<SedeCourse>();
    
     private List<Sede> SedeList { get; set; } = new();
     
     private SedeAssignmentViewModel ViewModel { get; set; } = new();
     
     private IList<SedeCourse>? SelectedItems { get; set; } = new List<SedeCourse>();
     
     private RadzenDataGrid<SedeCourse> _grid = new();
     
     private bool _isLoading;
     
     
     protected override async Task OnParametersSetAsync()
     {
         _isLoading = true;
         Course = _courseService.Find(CourseId);
         if (Course == default)
         {
           
             _courseNotFound = true;
         }else{
             ItemsCollection = await _sedeAssignmentService.GetSedeInCurse(Course.Id);
         }
         _isLoading = false;
         await base.OnParametersSetAsync();
     }

     private async void AddItem()
     {
         if (Course != null)
         {
            
             SedeList =  _sedeService.Find().ToList();
             ViewModel = new()
             {
                 CourseId = Course.Id
             };
             await ShowFormularyDialog(AddFormSubmit, "Asignar curso a Cedes");
         }
     }
     
     private async void AddFormSubmit()
     {
         _dialogService.Close();
         var openDialogTask = BusyDialog("Guardando ...");

         var response = await _sedeAssignmentService.Create(ViewModel);
         if (response.Status && response.Element != null)
         {
          
             NotifyOk(response.Title);
         }
         else
         {
             NotifyErrors(response.Title, response.Errors);
         }
     }
     
     private async void DeleteItems()
     {
         var confirmResult = await _dialogService.Confirm("Confirma  eliminar este curso de las Cedes seleccionadas?",
             "Confirmación", new ConfirmOptions { OkButtonText = "Si", CancelButtonText = "No" });

         
         if (confirmResult.HasValue && confirmResult.Value)
         {
             var openDialogTask = BusyDialog("Eliminando ...");

             if (SelectedItems != null)
             {
                 var response = await _sedeAssignmentService.Delete(SelectedItems);
                 if (response.Status)
                     NotifyOk(response.Title);
                 else
                     NotifyErrors(response.Title, response.Errors);
             }

             SelectedItems = new List<SedeCourse>();
         }
     }
     
     private async void NotifyOk(string title)
     {
         if (Course != null) ItemsCollection = await _sedeAssignmentService.GetSedeInCurse(Course.Id);
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
     
}