using MapaAndMaya.Services.Models;

namespace MapaAndMaya.Services.ViewModels;

public class CourseWithSubjectsViewModel
{
    public CourseWithSubjectsViewModel(int id, int yearsNumber, Modality modality, Degree degree, ICollection<SubjectsByYearViewModel> subjectsByYears)
    {
        Id = id;
        YearsNumber = yearsNumber;
        Modality = modality;
        Degree = degree;
        if (subjectsByYears.Count==yearsNumber)
        {
            SubjectsByYears = subjectsByYears;
        }
        else
        {
            throw new ArgumentException("La colección de asignaturas por año es diferente al numero de años del curso",nameof(@SubjectsByYears));
        }
        
    }
    

    public int Id { get; set; }
    
    public int YearsNumber { get; set; }
    
    public Modality Modality { get; set; }
    
    public Degree Degree { get; set; }

    public ICollection<SubjectsByYearViewModel> SubjectsByYears { get; }

    public static CourseWithSubjectsViewModel Generate(Course entity)
    {
        List<SubjectsByYearViewModel> list = new List<SubjectsByYearViewModel>();
        for (int i = 0; i < entity.YearsNumber; i++)
        {
            list.Add(new SubjectsByYearViewModel
            {
                Year = i
            });
        }
        CourseWithSubjectsViewModel model = new CourseWithSubjectsViewModel
        (
            entity.Id,
            entity.YearsNumber,
            entity.Modality ?? throw new InvalidOperationException(),
            entity.Degree ?? throw new InvalidOperationException(),
            list
        );
        return model;
    }
}