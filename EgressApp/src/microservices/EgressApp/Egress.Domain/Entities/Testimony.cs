namespace Egress.Domain.Entities;

public class Testimony : BaseEntity
{
    public string Content { get; set; }
    
    public bool WasAccepted { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}