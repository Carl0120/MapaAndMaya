using MapaAndMaya.Services;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace MapaAndMaya.Services.Tests;

public class CourseOfCumFumServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CourseOfCumFumServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task METHOD()
    {
        MapaAndMayaDbContextFactory factory = new MapaAndMayaDbContextFactory();
        MapaAndMayaDbContext context = factory.CreateDbContext(new []{" "});

        CourseOfCumFumService service = new CourseOfCumFumService(new NullLogger<CourseOfCumFumService>(), context);

        ICollection<Degree> degrees = await service.GetDegreeWithCourses();

        foreach (var degree in degrees)
        {
            foreach (var course in degree.Courses)
            {
                _testOutputHelper.WriteLine($"{course.DegreeId} {course.Modality?.Name}");
            }
        }
    }
}