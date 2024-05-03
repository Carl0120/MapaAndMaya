using System.ComponentModel.DataAnnotations.Schema;

namespace MapaAndMaya.Services.Models;

public class Course
{
    public int Id { get; set; }
    
    public bool Universalized { get; set; }
    
    public int YearsNumber { get; set; }
    
    public int DegreeId { get; set; }
    
    public int ModalityId { get; set; }

    public Modality? Modality { get; init; }
    
    public Degree? Degree { get; init; }
    
    public ICollection<Group> Groups { get; } = new List<Group>();

    public int Enrollment
    {
        get
        {
            int enrollment = 0;
            foreach (var group in Groups)
            {
                enrollment += group.Enrollment;
            }

            return enrollment;
        }
    }
}