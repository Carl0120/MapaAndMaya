using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;

namespace MapaAndMaya.Services.Mappers;

public static class CourseMapper
{
    public static CourseWithSubjectsViewModel MapToViewModel(Course entity)
    {
        List<SubjectsByYearViewModel> list = new List<SubjectsByYearViewModel>();
       
        for (int i = 1; i <= entity.YearsNumber; i++)
        {
            list.Add( MapToViewModel(i,entity.SubjectInCourses.Where(e=>e.Year==i).ToList()));
        }
        CourseWithSubjectsViewModel model = new CourseWithSubjectsViewModel
        (
            entity.Id,
            entity.YearsNumber,
            entity.Modality ?? throw new InvalidOperationException(),
            entity.Degree ?? throw new InvalidOperationException(),
            list
        );
        
       

        return model;
    }
    
    public static SubjectsByYearViewModel MapToViewModel(int year, List<SubjectInCourse> list)
    {
        if (list.Any(e => e.Year != year))
            throw new InvalidOperationException();
            
            
        SubjectsByYearViewModel model = new SubjectsByYearViewModel
        { 
           Year = year,
           SubjectsOfFirstPeriod =list.Where(e=>!e.Period).ToList(),
           SubjectsOfSecondPeriod = list.Where(e=>e.Period).ToList()
        };
        
        return model;
    }
}