using Egress.Domain.Enums;

namespace Egress.Domain.Entities;

public class PersonCourse : BaseEntity
{
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Mat { get; set; }

    public Level Level { get; set; }
    
    public Modality Modality { get; set; }
    
    #region Relationship
    public virtual Guid PersonId { get; set; }
    public Person Person { get; set; }
    
    public virtual Guid CourseId { get; set; }
    public Course Course { get; set; }
    #endregion
}