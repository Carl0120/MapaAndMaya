using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;

namespace MapaAndMaya.Services.Mappers;

public static class Extensions
{
    public static void ToEntity(this GenericViewModel model, NomenclatureBase entity)
    {
        entity.Name = model.Name;
    }

    public static void ToEntity(this AcademicYearViewModel model, AcademicYear entity)
    {
        entity.Name = model.Name;
        entity.Order = model.Order;
    }

    public static void ToEntity(this SedeViewModel model, Sede entity)
    {
        entity.Name = model.Name;
        entity.TownId = model.TownId;
        entity.SedeTypeId = model.SedeTypeId;
    }

    public static void ToEntity(this CourseViewModel model, Course entity)
        {
            entity.AcademicCourseId = model.AcademicCourseId;
            entity.DegreeModalityId = model.DegreeModalityId;
            entity.StudyPlanId = model.StudyPlanId;
        }
    
    public static void ToEntity(this YearsAssignmentViewModel model, YearsInCourse entity)
    {
        entity.AcademicYearId = model.AcademicYearId;
        entity.CourseId = model.CourseId;

        ICollection<PeriodInYear> periodInYears = new List<PeriodInYear>();
        
        foreach (var periodId in model.PeriodsId)
        {
            periodInYears.Add(new PeriodInYear
            {
                PeriodId = periodId,
                YearsInCourseId = entity.Id
            });
        }
        entity.PeriodInYears = periodInYears;
    }
    
    public static void ToEntity(this SubjectsAssignmentViewModel model,  List<SubjectsInPeriod> entities)
    {
        foreach (var item in model.SubjectsIdList)
        {
            var entity = new SubjectsInPeriod
            {
PeriodInYearId = model.PeriodInYearId,
SubjectId = item
            };
            entities.Add(entity);
        }
        
    }
   
}