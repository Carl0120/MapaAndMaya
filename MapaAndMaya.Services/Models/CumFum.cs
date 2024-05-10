namespace MapaAndMaya.Services.Models;

public class CumFum
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
    
    public Town Town { get; init; } = null!;

    public ICollection<CourseInCumFum> CourseInCumFum { get; } = new List<CourseInCumFum>();
}