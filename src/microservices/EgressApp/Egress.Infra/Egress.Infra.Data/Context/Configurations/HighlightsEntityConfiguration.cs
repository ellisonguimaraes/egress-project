using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Highlights ef configuration
/// </summary>
public class HighlightsEntityConfiguration : BaseEntityConfiguration<Highlights>
{
    #region Constants
    private const string TABLE_NAME = "Highlights";
    private const string TITLE_DB_PROPERTY_NAME = "title";
    private const byte TITLE_DB_PROPERTY_LENGTH = 200;
    private const string DESCRIPTION_DB_PROPERTY_NAME = "description";
    private const string LINK_DB_PROPERTY_NAME = "link";
    private const byte LINK_DB_PROPERTY_LENGTH = 200;
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    private const byte PERSON_ID_DB_PROPERTY_LENGTH = 36;
    #endregion
    
    public override void Configure(EntityTypeBuilder<Highlights> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(e => e.Title)
            .HasColumnName(TITLE_DB_PROPERTY_NAME)
            .HasMaxLength(TITLE_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.Description)
            .HasColumnName(DESCRIPTION_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.Link)
            .HasColumnName(LINK_DB_PROPERTY_NAME)
            .HasMaxLength(LINK_DB_PROPERTY_LENGTH);
        
        builder.Property(e => e.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .HasMaxLength(PERSON_ID_DB_PROPERTY_LENGTH)
            .IsRequired();

        builder.HasOne(e => e.Person)
            .WithMany(p => p.Highlights)
            .HasForeignKey(e => e.PersonId);
        
        base.Configure(builder);
    }
}