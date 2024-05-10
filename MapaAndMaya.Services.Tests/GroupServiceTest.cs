using MapaAndMaya.Services;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace MapaAndMaya.Services.Tests;

public class GroupServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public GroupServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }


    [Fact]
    public void GetByFaculty()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "output.txt");
        
      
    }

    
    
    private static CourseOfCumFumService InstanceService()
    {
        MapaAndMayaDbContextFactory factory = new MapaAndMayaDbContextFactory();
        MapaAndMayaDbContext context = factory.CreateDbContext(new []{" "});
        
      
        return new CourseOfCumFumService(new NullLogger<CourseOfCumFumService>(),context);
    }
}