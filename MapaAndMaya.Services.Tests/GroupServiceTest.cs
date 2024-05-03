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
        
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Configura la salida estándar para que escriba en el escritor de archivo
            Console.SetOut(writer);

        CourseService service = InstanceService();
        ICollection<Course> course =  service.Get(1).Result;
             _testOutputHelper.WriteLine(course.Count.ToString());
         
        foreach (var curso in course)
        {
            _testOutputHelper.WriteLine($"Curso# {curso.Id} :{curso.Degree.Name}; " +
                                        $"{curso.Modality.Name}; " +
                                        $"{curso.Enrollment} " 
                                       );
            _testOutputHelper.WriteLine($"  Grupos del Curso# {curso.Id} = {curso.Groups.Count}");
            foreach (var grupo in curso.Groups)
            {
                _testOutputHelper.WriteLine($" Grupo# {grupo.Id}: " +
                                            $"{grupo.CumFumId}; " +
                                            $"{grupo.CourseId}  " +
                                            $"{grupo.AcademicYear} " +
                                            $"{grupo.Enrollment}" 
                );
            }


        }
        Console.SetOut(Console.Out);
        }
    }

    
    
    private static CourseService InstanceService()
    {
        MapaAndMayaDbContextFactory factory = new MapaAndMayaDbContextFactory();
        MapaAndMayaDbContext context = factory.CreateDbContext(new []{" "});
        
      
        return new CourseService(new NullLogger<FacultyService>(),context);
    }
}