using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Maya.Pages;

public partial class Subjects
{
    [Parameter] public int CourseId { get; set; }
    private bool DisableButton = true;
    private CourseWithSubjectsViewModel? Course { get; set; }
    private List<SubjectInCourse> ItemsChanged { get; set; } = new();
    RadzenTabs tabs;

    protected override async Task OnParametersSetAsync()
    {
        Course = await _SubjectInCourseService.GetCourseWithSubjects(CourseId);
        await base.OnParametersSetAsync();
    }

    private async Task ChangeExamsState()
    {
            await  _SubjectInCourseService.SaveChanges();
            ItemsChanged = new List<SubjectInCourse>();
            DisableButton = !ItemsChanged.Any();
    }

    private void RestartChanges()
    {
        foreach (var item in ItemsChanged)
        {
            item.HaveFinalExam = !item.HaveFinalExam;
        }

        ItemsChanged = new List<SubjectInCourse>();
        DisableButton = !ItemsChanged.Any();
        StateHasChanged();
    }
}