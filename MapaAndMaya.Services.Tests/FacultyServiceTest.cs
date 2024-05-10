using System.Configuration;
using System.Drawing;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using MapaAndMaya.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;
using Faculty = MapaAndMaya.Services.Models.Faculty;

namespace MapaAndMaya.Services.Tests;


public class FacultyServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public FacultyServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async void Insert()
    {
        MapaAndMayaDbContextFactory factory = new MapaAndMayaDbContextFactory();
        MapaAndMayaDbContext context = factory.CreateDbContext(new []{" "});
        
        FacultyService service = new FacultyService(new NullLogger<FacultyService>(),context);

      ActionResult<Faculty> result = await service.Create(new FacultyViewModel
        {    
            Name = "Facultad Nueva"
        });
      _testOutputHelper.WriteLine(result.Element.Id.ToString());
      if (!result.Status)
      {
          foreach (var error in result.Errors)
          {
              _testOutputHelper.WriteLine(error);
          }
      }
      Assert.True(result.Element.Id > 0 );
    }

    [Fact]
    public async void FindAll()
    {
        MapaAndMayaDbContextFactory factory = new MapaAndMayaDbContextFactory();
        MapaAndMayaDbContext context = factory.CreateDbContext(new []{" "});
        
        FacultyService service = new FacultyService(new NullLogger<FacultyService>(),context);

        Faculty? list = await service.Find(30);
        
        
            _testOutputHelper.WriteLine($"{list.Id} {list.Name}");
       
        Assert.NotNull(list);
    }
    }
    