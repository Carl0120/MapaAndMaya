using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;

namespace MapaAndMaya.Services.Mappers;

public static class CourseMapper
{
    public static CourseWithSubjectsViewModel MapToViewModel(Course entity)
    {
        List<SubjectsByYearViewModel> list = new List<SubjectsByYearViewModel>();
        for (int i = 0; i < entity.YearsNumber; i++)
        {
            list.Add(new SubjectsByYearViewModel
            {
                Year = i
            });
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
    
    public static SubjectsByYearViewModel MapToViewModel(List<SubjectInCourse> list)
    {
        if (list.Count == 0 || list.Any(e => e.Year != list[0].Year))
            throw new InvalidOperationException();
            
            
        SubjectsByYearViewModel model = new SubjectsByYearViewModel
        { 
           Year = list[0].Year,
           SubjectsOfFirstPeriod =list.Where(e=>!e.Period).ToList(),
           SubjectsOfSecondPeriod = list.Where(e=>e.Period).ToList()
        };
        

        return model;
    }
}