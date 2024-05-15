using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Mappers;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class SubjectInCourseService
{
    
    private readonly ILogger<SubjectInCourseService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public SubjectInCourseService(ILogger<SubjectInCourseService> logger, MapaAndMayaDbContext dbContext)
    {
        this._logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<CourseWithSubjectsViewModel?> GetCourseWithSubjects(int courseId)
    {
        try
        {
            Course? course = await _dbContext.Courses
                .Include(e=>e.Degree)
                .Include(e=>e.Modality)
                .Include(e=>e.SubjectInCourses)
                .ThenInclude(e=>e.Subject)
                .FirstOrDefaultAsync(e=>e.Id==courseId);
            if (course == null) return null;

            CourseWithSubjectsViewModel model = CourseMapper.MapToViewModel(course);
            return model;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<SubjectsByYearViewModel?> GetSubjectsByYear(int courseId, int year)
    {
        try
        {
            List<SubjectInCourse> subjects  = await _dbContext.SubjectInCourses
                .Where(e=>e.Id==courseId && e.Year==year)
                .Include(e=>e.Subject)
                .ToListAsync();
           
            if (!subjects.Any()) return null;
            

            SubjectsByYearViewModel model = CourseMapper.MapToViewModel(year,subjects);
            return model;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
   
    public async Task<ICollection<Course>> GetCourseByFaculty(int facultyId)
    {
        try
        {
            var query = _dbContext.Courses
                .Include(e => e.Degree)
                .Include(e => e.Modality)
                .Where(course => course.Degree != null && course.Degree.FacultyId == facultyId)
                .AsNoTracking();
            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<Course>();
        }
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
    
}