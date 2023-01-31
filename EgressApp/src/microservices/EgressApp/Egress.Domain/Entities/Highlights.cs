namespace Egress.Domain.Entities;

public class Highlights : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string? Link { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}