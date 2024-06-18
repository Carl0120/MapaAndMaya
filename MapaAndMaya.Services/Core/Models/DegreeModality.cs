namespace MapaAndMaya.Services.Core.Models;

public class DegreeModality
{
    public int Id { get; set; }

    public int ModalityId { get; init; }
    public Modality? Modality { get; init; }

    public int DegreeId { get; init; }
    public Degree? Degree { get; init; }

    public ICollection<Course> Courses { get; } = new List<Course>();
}