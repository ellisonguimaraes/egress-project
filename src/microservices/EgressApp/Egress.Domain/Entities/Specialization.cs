using Egress.Domain.Enums;

namespace Egress.Domain.Entities;

public class Specialization : BaseEntity
{
    public string CourseName { get; set; }
    
    public string InstitutionName { get; set; }

    public bool Status { get; set; }

    public Modality Modality { get; set; }
    
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}