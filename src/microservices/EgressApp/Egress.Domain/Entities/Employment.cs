using Egress.Domain.Enums;

namespace Egress.Domain.Entities;

public class Employment : BaseEntity
{
    public string Role { get; set; }

    public string Enterprise { get; set; }

    public string Section { get; set; }
    
    public decimal? SalaryRange { get; set; }

    public Initiative Initiative { get; set; }

    public bool Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}