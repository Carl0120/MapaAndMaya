using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class Modality
{
    
    public int Id { get; set; }

    [MaxLength(50)] 
    public string Name { get; set; } = "";
    
    public ICollection<Course> DegreeModes { get; } = new List<Course>();
}