using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;

namespace MapaAndMaya.Services.Mappers;

public static class Extensions
{
    public static void CopyToEntity(this FacultyViewModel model, Faculty entity)
    {
        entity.Name = model.Name;
    }
    
    public static void CopyToEntity(this DegreeViewModel model, Degree entity)
    {
        entity.Name = model.Name;
        entity.AccreditationStatus = model.AccreditationStatus;
        entity.FacultyId = model.FacultyId;
    }
    
    public static void CopyToEntity(this GroupToCourseRequest model, Group entity)
    {
        entity.CourseInCumFumId = model.CourseInCumFumId;
        entity.AcademicYear = model.AcademicYear;
        entity.Enrollment = model.Enrollment;
        entity.AcademicCourse = model.AcademicCourse;
    }

    public static void Clone(this Group source, Group origin)
    {
        source.AcademicYear = origin.AcademicYear;
        source.Enrollment = origin.Enrollment;
        source.AcademicCourse = origin.AcademicCourse;
    }
    
}