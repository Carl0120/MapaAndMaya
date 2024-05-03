using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public enum AccreditationStatus
{
    A,
    B,
    C
}

public class Degree
{
    public int Id { get; init; }
    
    public string Name { get; set; } = "";
    
    public AccreditationStatus? AccreditationStatus { get; set; }
    
    public int FacultyId { get; set; }

    public Faculty? Faculty { get; set; }

    public ICollection<Course> Courses { get; } = new List<Course>();
}