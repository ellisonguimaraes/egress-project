namespace Egress.Domain.Entities;

public class Address : BaseEntity
{
    public string? Street { get; set; }

    public string? District { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}