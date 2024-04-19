using System.ComponentModel.DataAnnotations;

namespace MapaAndMaya.Services.Models;

public class Faculty
{
    public int Id { get; set; }

    [MaxLength(100)] 
    public string Name { get; set; }

    public ICollection<Degree>? Degrees { get; set; }
}