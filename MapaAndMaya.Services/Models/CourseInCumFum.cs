namespace MapaAndMaya.Services.Models;

public class CourseInCumFum
{
    public int Id { get; set; }
    
    public int CumFumId { get; set; }

    public int CourseId { get; set; }

    public CumFum? CumFum { get; set;}
    
    public Course? Course { get; set;}
    
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