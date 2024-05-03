using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MapaAndMaya.Services;

public class CourseService
{
    
    private readonly ILogger<FacultyService> _logger;

    private readonly MapaAndMayaDbContext _dbContext;

    public CourseService(ILogger<FacultyService> logger, MapaAndMayaDbContext dbContext)
    {
        this._logger = logger;
        _dbContext = dbContext;
    }

    public async Task<List<Course>> FindAll()
    {
        List<Course> courses = await _dbContext.Courses
            .Include(e =>e.Modality)
            .Include(e =>e.Degree)
            .ThenInclude(e=>e.Faculty)
            .ToListAsync();
        return courses;
    }
   
    public async Task<ICollection<Course>> Get(int cumFumId)
    {
        var query = _dbContext.Courses
            .Where(course => course.Groups.Any(group => group.CumFumId == cumFumId))
            .Include(e => e.Groups.Where(e=>e.CumFumId==cumFumId))
            .Include(e => e.Modality)
            .Include(e => e.Degree)
            .ThenInclude(e => e.Faculty)
            .AsNoTracking();
        return await query.ToListAsync();
    }
    public async Task<List<Degree>> GetByDegree(int degreeId)
    {
        List<Degree> fums = await _dbContext.Degrees.Where(e=>e.Id == degreeId)
            .Include(e =>e.Courses)
            .ThenInclude(e=> e.Groups)
            .ToListAsync();
        return fums;
    }
    
}