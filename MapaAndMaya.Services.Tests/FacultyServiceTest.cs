using System.Configuration;
using MapaAndMaya.Services.DB;
using MapaAndMaya.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MapaAndMaya.Services.Tests;


public class FacultyServiceTest
{
    
    [Fact]
    public  void FindAll()
    {
        var serviceCollection = new ServiceCollection();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<FacultyService>();
        var colection = service.FindAll().Result;
        Console.WriteLine(colection.First(e => e.Id == 1));
        
        Assert.True(colection.Any());
    }
    }
    